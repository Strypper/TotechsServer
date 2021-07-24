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
    public class UserTeamController : BaseController
    {
        public IMapper _mapper;
        public IUserTeamRepository _userTeamRepository;
        public IUserRepository _userRepository;
        public ITeamRepository _teamRepository;

        public UserTeamController(IMapper mapper,
                                  IUserTeamRepository userTeamRepository, 
                                  IUserRepository userRepository,
                                  ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _userTeamRepository = userTeamRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var userTeams = await _userTeamRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserTeamDTO>>(userTeams));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var userTeam = await _userTeamRepository.FindByIdAsync(id, cancellationToken);
            if (userTeam is null) return NotFound();
            return Ok(_mapper.Map<UserTeamDTO>(userTeam));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTeamByUser(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user is null) return NotFound();
            var userTeams = await _userTeamRepository.FindAll(ut => ut.User == user).ToListAsync();

            var teams = new List<Team>();
            foreach (var userTeam in userTeams)
            {
                teams.Add(userTeam.Team);
            }
            return Ok(_mapper.Map<IEnumerable<TeamDTO>>(teams));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateUserTeamDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.UserId, cancellationToken);
            if (user is null || user.IsDisable == true) return NotFound();
            var team = await _teamRepository.FindByIdAsync(dto.TeamId, cancellationToken);
            var userTeam = new UserTeam() { User = user, Team = team };
            if (await _userTeamRepository.FindByUserId(user.Id, cancellationToken) != null)
            {
                var existingUserTeam = await _userTeamRepository.FindAll(uf => uf.User.Id == dto.UserId).FirstOrDefaultAsync();
                if (existingUserTeam.TeamId == dto.TeamId)
                    return BadRequest("This user and team are already created !!");
                else _userTeamRepository.Create(userTeam);
            }
            else _userTeamRepository.Create(userTeam);
            await _userTeamRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { userTeam.Id }, _mapper.Map<UserTeamDTO>(userTeam));
        }
        [HttpPut]
        public async Task<IActionResult> Update(CreateUpdateUserTeamDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.UserId, cancellationToken);
            if (user is null) return NotFound();
            var userDTO = _mapper.Map<UserDTO>(user);
            var food = await _teamRepository.FindByIdAsync(dto.TeamId, cancellationToken);
            if (food is null) return NotFound();
            var teamDTO = _mapper.Map<TeamDTO>(food);
            var userTeam = new UserTeamDTO() { User = userDTO, Team = teamDTO };
            _userTeamRepository.Update(_mapper.Map<UserTeam>(userTeam));
            await _userTeamRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var userTeam = await _userTeamRepository.FindByIdAsync(id, cancellationToken);
            if (userTeam is null) return NotFound();
            _userTeamRepository.Delete(userTeam);
            await _userTeamRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
