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
        public IMapper _mapper;
        public IUserRepository _userRepository;
        public IConversationRepository _conversationRepository;
        public IUserConversationRepository _userConversationRepository;
        public ConversationController(IMapper mapper,
                                      IUserRepository userRepository,
                                      IConversationRepository conversationRepository,
                                      IUserConversationRepository userConversationRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _conversationRepository = conversationRepository;
            _userConversationRepository = userConversationRepository;
        }

        [HttpGet]
        //[Authorize(Policy = "LOLPermission")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var conversations = await _conversationRepository.FindAll()
                                                             .Include(conversation => conversation.ChatMessages)
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

        [HttpGet("{userId}/{pageIndex}")]
        public async Task<IActionResult> GetByUserIdDirectMode(string userId, int pageIndex, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.FindByIdAsync(userId, cancellationToken);
            var userConverastions = await _userConversationRepository.FindAll(uc => uc.UserId.Equals(userId))
                                                                     .ToListAsync(cancellationToken);
            //Find all the conversationId based on the userConversations
            var conversationIds = userConverastions.Select(userConverastions => userConverastions.ConversationId);
            var conversations = await _conversationRepository.FindAll(c => conversationIds.Contains(c.Id))
                                                                 .Include(conversation => conversation.ChatMessages)
                                                                 .ThenInclude(chatmessage => chatmessage.User)
                                                                    .Skip(pageIndex * 10).Take(10)
                                                                 .OrderByDescending(conversation => conversation.LastInteractionTime)
                                                                 .ToListAsync(cancellationToken);
            var conversationDirectModeDTOList = new List<ConversationDirectModeDTO>();
            foreach (var conversation in conversations)
            {
                var targetUserId = await _userConversationRepository
                                            .FindAll(uc => uc.ConversationId == conversation.Id)
                                            .Where(uc => uc.UserId != currentUser.Id)
                                            .Select(uc => uc.UserId).FirstOrDefaultAsync(cancellationToken);
                var targetUser = await _userRepository.FindByIdAsync(targetUserId, cancellationToken);
                var chatMessageList = new List<ChatMessageDTO>();
                foreach (var chatMessage in conversation.ChatMessages)
                {
                    chatMessageList.Add(new ChatMessageDTO()
                    {
                        Id = chatMessage.Id,
                        User = _mapper.Map<UserDTO>(chatMessage.User),
                        MessageContent = chatMessage.MessageContent,
                        SentTime = chatMessage.SentTime
                    });
                }



                var conversationDirectModeDTO = new ConversationDirectModeDTO()
                {
                    Id = conversation.Id,
                    ChatMessages = chatMessageList,
                    DateCreated = conversation.DateCreated,
                    LastInteractionTime = conversation.LastInteractionTime,
                    LastMessageContent = conversation.LastMessageContent,
                    Users = new List<UserDTO>() { _mapper.Map<UserDTO>(currentUser), _mapper.Map<UserDTO>(targetUser) }
                };
                conversationDirectModeDTOList.Add(conversationDirectModeDTO);
            }
            return Ok(conversationDirectModeDTOList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateConversationDTO dto, CancellationToken cancellationToken = default)
        {
            var currentUser = await _userRepository.FindByIdAsync(dto.CurrentUserId, cancellationToken);
            var targerUser = await _userRepository.FindByIdAsync(dto.TargerUserId, cancellationToken);


            var conversation = new Conversation();
            _conversationRepository.Create(conversation);
            await _conversationRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { conversation.Id }, _mapper.Map<ConversationDTO>(conversation));
        }
    }
}