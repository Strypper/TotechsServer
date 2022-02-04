using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Database;
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
    public class UserConversationController : BaseController
    {
        public IMapper                     _mapper;
        public IUserRepository             _userRepository;
        public IntranetContext             _intranetContext;
        public IConversationRepository     _conversationRepository;
        public IUserConversationRepository _userConversationRepository;

        public UserConversationController(IMapper mapper,
                                          IUserRepository userRepository,
                                          IntranetContext intranetContext,
                                          IConversationRepository conversationRepository,
                                          IUserConversationRepository userConversationRepository)
        {
            _mapper                     = mapper;
            _userRepository             = userRepository;
            _intranetContext            = intranetContext;
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
            //Find the current user
            var currentUser = await _userRepository.FindByIdAsync(dto.CurrentUserId, cancellationToken);
            //Find the target user
            var targetUser  = await _userRepository.FindByIdAsync(dto.TargetUserId,  cancellationToken);
            //Check if the conversation of between these people
            //Get all the User-Conversation from current user
            var currentUserConversations = await _userConversationRepository.FindAll(ucr => ucr.UserId == dto.CurrentUserId).Select(ucr => ucr.ConversationId).ToListAsync();
            var targetUserConversations  = await _userConversationRepository.FindAll(ucr => ucr.UserId == dto.TargetUserId).Select(ucr => ucr.ConversationId).ToListAsync();
            //If the conversation not exist
            if (currentUserConversations.Intersect(targetUserConversations).Any() == false)
            {
                using var intranetTransaction = await _intranetContext.Database.BeginTransactionAsync();
                var newConversation = new Conversation() { Users = new List<User>() { currentUser, targetUser } };
                _conversationRepository.Create(newConversation);
                await _conversationRepository.SaveChangesAsync(cancellationToken);
                var currentUserConversation = new UserConversation()
                {
                    User = currentUser,
                    Conversation = newConversation
                };
                _userConversationRepository.Create(currentUserConversation);
                var targerUserConversation = new UserConversation()
                {
                    User = targetUser,
                    Conversation = newConversation
                };
                _userConversationRepository.Create(targerUserConversation);
                await _userConversationRepository.SaveChangesAsync(cancellationToken);
                await _intranetContext.Database.CommitTransactionAsync(cancellationToken);
                return Ok(newConversation);
            }
            else
            {
                //If the conversation do exist send the existing conversation-id
                return Ok(currentUserConversations.Intersect(targetUserConversations).First());
            }
        }
        //[HttpPut]
        //public async Task<IActionResult> Update(CreateUpdateUserConversationDTO dto, CancellationToken cancellationToken = default)
        //{
        //    var user = await _userRepository.FindByIdAsync(dto.UserId, cancellationToken);
        //    if (user is null) return NotFound();
        //    var userDTO = _mapper.Map<UserDTO>(user);
        //    var conversation = await _conversationRepository.FindByIdAsync(dto.ConversationId, cancellationToken);
        //    if (conversation is null) return NotFound();
        //    var conversationDTO = _mapper.Map<ConversationDTO>(conversation);
        //    var userConversation = new UserConversationDTO() { User = userDTO, Conversation = conversationDTO };
        //    _userConversationRepository.Update(_mapper.Map<UserConversation>(userConversation));
        //    await _userConversationRepository.SaveChangesAsync(cancellationToken);
        //    return NoContent();
        //}
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
