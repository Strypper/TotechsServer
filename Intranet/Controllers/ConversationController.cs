using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class ConversationController : BaseController
    {
        public IMapper                 _mapper;
        public IUserRepository         _userRepository;
        public IConversationRepository _conversationRepository;
        public ConversationController(IMapper mapper,
                                      IUserRepository userRepository,
                                      IConversationRepository conversationRepository)
        {
            _mapper                 = mapper;
            _userRepository         = userRepository;
            _conversationRepository = conversationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var conversations = await _conversationRepository.FindAll()
                                                             .Include(conversation => conversation.ChatMessages)
                                                                .Take(10)
                                                             .Include(conversation => conversation.Users)
                                                             .ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ConversationDTO>>(conversations));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var conversation = await _conversationRepository.FindByIdAsync(id, cancellationToken);
            if (conversation is null) return NotFound();
            return Ok(_mapper.Map<ConversationDTO>(conversation));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int id, CancellationToken cancellationToken)
        {
            var user          = await _userRepository.FindByIdAsync(id, cancellationToken);
            var conversations = await _conversationRepository.FindAll(c => c.Users.Contains(user))
                                                              .Include(conversation => conversation.ChatMessages)
                                                                .Take(10)
                                                             .Include(conversation => conversation.Users)
                                                             .ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ConversationDTO>>(conversations));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateConversationDTO dto, CancellationToken cancellationToken = default)
        {
            var currentUser = await _userRepository.FindByIdAsync(dto.CurrentUserId, cancellationToken);
            var targerUser  = await _userRepository.FindByIdAsync(dto.TargerUserId,  cancellationToken);


            var conversation = new Conversation() { Users = new List<User>() { currentUser, targerUser } };
            _conversationRepository.Create(conversation);
            await _conversationRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { conversation.Id }, _mapper.Map<ConversationDTO>(conversation));
        }
    }
}