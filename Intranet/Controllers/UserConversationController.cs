using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserConversationController : BaseController
    {
        public IMapper                     _mapper;
        public IUserRepository             _userRepository;
        public IConversationRepository     _conversationRepository;
        public IUserConversationRepository _userConversationRepository;

        public UserConversationController(IMapper mapper,
                                          IUserRepository userRepository,
                                          IConversationRepository conversationRepository,
                                          IUserConversationRepository userConversationRepository)
        {
            _mapper                     = mapper;
            _userRepository             = userRepository;
            _conversationRepository     = conversationRepository;
            _userConversationRepository = userConversationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var userConversations = await _userConversationRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserConversationDTO>>(userConversations));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var userConversation = await _userConversationRepository.FindByIdAsync(id, cancellationToken);
            if (userConversation is null) return NotFound();
            return Ok(_mapper.Map<UserConversationDTO>(userConversation));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserConversationByUserId(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user is null) return NotFound();
            var userConversations = await _userConversationRepository.FindByUserId(userId, cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserConversationDTO>>(userConversations));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateUserConversationDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.UserId, cancellationToken);
            if (user is null || user.IsDisable == true) return NotFound();
            var conversation = await _conversationRepository.FindByIdAsync(dto.ConversationId, cancellationToken);
            if (conversation is null) return NotFound();
            var userConversation = new UserConversation() { User = user, Conversation = conversation };
            if (await _userConversationRepository.FindByUserId(user.Id, cancellationToken) != null)
            {
                var existingUserConversation = await _userConversationRepository.FindAll(uf => uf.User.Id == dto.UserId).FirstOrDefaultAsync();
                existingUserConversation.ConversationId = dto.ConversationId;
                _userConversationRepository.Update(existingUserConversation);
            }
            else _userConversationRepository.Create(userConversation);
            await _userConversationRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { userConversation.Id }, _mapper.Map<UserConversationDTO>(userConversation));
        }
        [HttpPut]
        public async Task<IActionResult> Update(CreateUpdateUserConversationDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.UserId, cancellationToken);
            if (user is null) return NotFound();
            var userDTO = _mapper.Map<UserDTO>(user);
            var conversation = await _conversationRepository.FindByIdAsync(dto.ConversationId, cancellationToken);
            if (conversation is null) return NotFound();
            var conversationDTO = _mapper.Map<ConversationDTO>(conversation);
            var userConversation = new UserConversationDTO() { User = userDTO, Conversation = conversationDTO };
            _userConversationRepository.Update(_mapper.Map<UserConversation>(userConversation));
            await _userConversationRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var userConversation = await _userConversationRepository.FindByIdAsync(id, cancellationToken);
            if (userConversation is null) return NotFound();
            _userConversationRepository.Delete(userConversation);
            await _userConversationRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
