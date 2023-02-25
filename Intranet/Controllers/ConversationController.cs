namespace Intranet;

public class ConversationController : BaseController
{
    public IMapper _mapper;
    public IUserRepository _userRepository;
    public IChatMessageRepository _chatMessageRepository;
    public IConversationRepository _conversationRepository;
    public IUserConversationRepository _userConversationRepository;
    public ConversationController(IMapper mapper,
                                  IUserRepository userRepository,
                                  IChatMessageRepository chatMessageRepository,
                                  IConversationRepository conversationRepository,
                                  IUserConversationRepository userConversationRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _chatMessageRepository = chatMessageRepository;
        _conversationRepository = conversationRepository;
        _userConversationRepository = userConversationRepository;
    }

    [HttpGet]
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
    public async Task<IActionResult> GetByUserIdDirectMode(string userGuid, int pageIndex, CancellationToken cancellationToken)
    {
        var currentUser = await _userRepository.FindByGuidAsync(userGuid, cancellationToken);
        var userConverastions = await _userConversationRepository.FindAll(uc => uc.UserId.Equals(userGuid))
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
                                        .Where(uc => uc.UserId != currentUser!.Id)
                                        .Select(uc => uc.UserId).FirstOrDefaultAsync(cancellationToken);
            if (targetUserId is not null)
            {
                var targetUser = await _userRepository.FindByGuidAsync(targetUserId, cancellationToken);
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
        }
        return Ok(conversationDirectModeDTOList);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetLobbyRecentChatMessages(CancellationToken cancellationToken = default)
    {
        var mauislandLobby = await _conversationRepository.FindByNameAsync("MAUIslandLobby", cancellationToken);
        if (mauislandLobby is null)
            return BadRequest("Lobby chat is not created yet!");

        return Ok(mauislandLobby!.ChatMessages);
    }
    #region [POST]

    [HttpPost]
    public async Task<IActionResult> CreateDirectConversation(CreateConversationDTO dto, CancellationToken cancellationToken = default)
    {
        var currentUser = await _userRepository.FindByGuidAsync(dto.CurrentUserGuid, cancellationToken);
        var targerUser = await _userRepository.FindByGuidAsync(dto.TargerUserGuid, cancellationToken);


        var conversation = new Conversation();
        await _conversationRepository.CreateAsync(conversation);
        await _conversationRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { conversation.Id }, _mapper.Map<ConversationDTO>(conversation));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMAUIslandLobbyChat(CancellationToken cancellationToken = default)
    {
        var mauislandLobby = await _conversationRepository.FindByNameAsync("MAUIslandLobby", cancellationToken);
        if (mauislandLobby is not null)
            return BadRequest("Lobby already created !");

        var conversation = new Conversation() { Name = "MAUIslandLobby" };
        await _conversationRepository.CreateAsync(conversation);
        await _conversationRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { conversation.Id }, _mapper.Map<ConversationDTO>(conversation));
    }

    #endregion
}