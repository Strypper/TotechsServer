using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    //[Authorize(Policy = "IntranetPermission")]
    [Route("/api/[controller]/[action]")]
    public class UserController : BaseController
    {
        private IMapper _mapper;
        //private IMediaService               _mediaService;
        private IntranetContext _intranetContext;
        private IUserRepository _userRepository;
        private IChatMessageRepository _chatMessageRepository;
        private IConversationRepository _conversationRepository;
        private IUserConversationRepository _userConversationRepository;
        public UserController(IMapper mapper,
                              //IMediaService               mediaService,
                              IntranetContext intranetContext,
                              IUserRepository userRepository,
                              IChatMessageRepository chatMessageRepository,
                              IConversationRepository conversationRepository,
                              IUserConversationRepository userConversationRepository)
        {
            _mapper = mapper;
            //_mediaService               = mediaService;
            _intranetContext = intranetContext;
            _userRepository = userRepository;
            _chatMessageRepository = chatMessageRepository;
            _conversationRepository = conversationRepository;
            _userConversationRepository = userConversationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(id, cancellationToken);
            if (user is null) return NotFound();
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpGet("{singalRConnectionStringId}")]
        public async Task<IActionResult> GetUserBySingalRConnectionStringId(string singalRConnectionStringId, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(singalRConnectionStringId) && !string.IsNullOrWhiteSpace(singalRConnectionStringId))
            {
                var user = await _userRepository.FindBySignalRConnectionId(singalRConnectionStringId);
                return Ok(_mapper.Map<UserDTO>(user));

            }
            else return NotFound();
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetUserByGuid(string guid, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(guid) && !string.IsNullOrWhiteSpace(guid))
            {
                var user = await _userRepository.FindByGuid(guid, cancellationToken);
                return Ok(_mapper.Map<UserDTO>(user));

            }
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin loginInfo, CancellationToken cancellationToken = default)
        {
            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            var types = claim.Select(x => x.Type).ToList();
            var values = claim.Select(x => x.Value).ToList();
            var guid = values.ElementAt(types.IndexOf("Guid"));

            var user = await _userRepository.FindByUserName(loginInfo.UserName, cancellationToken);
            if (user is null || user.IsDisable == true) return NotFound();
            return Ok(_mapper.Map<UserDTO>(user));


            //var jwtConfig = new JwtTokenConfig();
            //return Ok(await GenerateToken(user, jwtConfig,DateTime.Now.AddMinutes(5)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO dto, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(dto);
            _userRepository.Create(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { user.Id }, _mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(string id, IFormFile avatar, CancellationToken cancellationToken = default)
        {
            //if (_mediaService.IsImage(avatar))
            //{
            //    var user = await _userRepository.FindByIdAsync(id, cancellationToken);
            //    if(user is null)
            //    {
            //        return NotFound($"No User With Id {id} Found!");
            //    }
            //    using (Stream stream = avatar.OpenReadStream())
            //    {
            //        Tuple<bool, string> result = await _mediaService.UploadAvatarToStorage(stream, avatar.FileName);
            //        var isUploaded = result.Item1;
            //        var stringUrl = result.Item2;
            //        if (isUploaded && !string.IsNullOrEmpty(stringUrl))
            //        {
            //            user.ProfilePic = stringUrl;
            //            await _userRepository.SaveChangesAsync(cancellationToken);

            //            return Ok(stringUrl);
            //        }
            //        else return BadRequest("Look like the image couldnt upload to the storage");
            //    }
            //}
            return new UnsupportedMediaTypeResult();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (user is null) return NotFound();
            _mapper.Map(dto, user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdatePassword(UserDTO dto, CancellationToken cancellationToken = default)
        //{
        //    if(!string.IsNullOrEmpty(dto.Password))
        //    {
        //        var user = await _userRepository.FindByIdAsync(dto.Id, cancellationToken);
        //        if (user is null) return NotFound();
        //        _mapper.Map(dto, user);
        //        await _userRepository.SaveChangesAsync(cancellationToken);
        //        return NoContent();
        //    } else { return NotFound(); }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(id, cancellationToken);
            if (user is null) return NotFound();
            using var intranetTransaction = await _intranetContext.Database.BeginTransactionAsync();
            foreach (var chatMessage in _chatMessageRepository.FindAll(cm => cm.User.Id == user.Id))
            {
                _chatMessageRepository.Delete(chatMessage);
            }

            var conversationIdToDelete = new List<int>();
            foreach (var userConversation in _userConversationRepository.FindAll(uc => uc.UserId == user.Id))
            {
                conversationIdToDelete.Add(userConversation.ConversationId);
            }

            foreach (var conversationId in conversationIdToDelete)
            {
                var conversation = await _conversationRepository.FindByIdAsync(conversationId, cancellationToken);
                _conversationRepository.Delete(conversation);
            }
            foreach (var conversationId in conversationIdToDelete)
            {
                var userConversation = await _userConversationRepository
                                                .FindAll(uc => uc.ConversationId == conversationId)
                                                .FirstOrDefaultAsync(cancellationToken);
                _userConversationRepository.Delete(userConversation);
            }
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            await _intranetContext.Database.CommitTransactionAsync(cancellationToken);
            return NoContent();
        }

        //private async Task<string> GenerateToken(User user, JwtTokenConfig jwtTokenConfig, DateTime expires)
        //{
        //    var handler = new JwtSecurityTokenHandler();

        //    var claims = new Claim[]
        //    {
        //        new Claim(ClaimTypes.Name, "Viet")
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("54653216554114442313244544"));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

        //    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        //    {
        //        Subject            = new ClaimsIdentity(claims),
        //        Issuer             = "Bruh1",
        //        Audience           = "Bruh",
        //        SigningCredentials = creds,
        //        Expires            = expires
        //    });

        //    return handler.WriteToken(securityToken);
        //}
    }
}
