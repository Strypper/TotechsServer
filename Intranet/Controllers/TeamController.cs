using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class TeamController : BaseController
    {
        public IMapper _mapper;
        public IUserTeamRepository _userTeamRepository;
        public IUserRepository _userRepository;
        public ITeamRepository _teamRepository;
        public IntranetContext _intranetContext { get; set; }
        public TeamController(IMapper mapper,
                              IUserTeamRepository userTeamRepository,
                              IUserRepository userRepository,
                              ITeamRepository teamRepository,
                              IntranetContext intranetContext)
        {
            _mapper = mapper;
            _userTeamRepository = userTeamRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _intranetContext = intranetContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var teams = await _teamRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<TeamDTO>>(teams));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeamsWithMembers(CancellationToken cancellationToken = default)
        {
            var teams = await _teamRepository.FindAll().ToListAsync(cancellationToken);
            var teamsDTO = _mapper.Map<IEnumerable<TeamDTO>>(teams);
            var teamsWithUserDTO = new List<TeamWithMemberDTO>();
            foreach (var teamDTO in teamsDTO)
            {
                var userTeamDTO = await _userTeamRepository
                                        .FindAll(ut => ut.TeamId == teamDTO.Id)
                                        .ToListAsync();
                var members = userTeamDTO.Where(ut => ut.Team.Id == teamDTO.Id)
                                         .Select(ut => ut.User);
                var teamsWithMembers = new TeamWithMemberDTO()
                {
                    Id = teamDTO.Id,
                    TeamName = teamDTO.TeamName,
                    Clients = teamDTO.Clients,
                    About = teamDTO.About,
                    Company = teamDTO.Company,
                    TechLead = teamDTO.TechLead,
                    Members = _mapper.Map<IEnumerable<UserDTO>>(members)
                };
                teamsWithUserDTO.Add(teamsWithMembers);
            }
            return Ok(teamsWithUserDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var team = await _teamRepository.FindByIdAsync(id, cancellationToken);
            if (team is null) return NotFound();
            return Ok(_mapper.Map<TeamDTO>(team));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamDTO dto, CancellationToken cancellationToken = default)
        {
            var team = _mapper.Map<Team>(dto);
            _teamRepository.Create(team);
            await _teamRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { team.Id }, _mapper.Map<TeamDTO>(team));
        }

        [HttpPost]
        public async Task<IActionResult> CreatTeamWithUsers(CreateTeamWithMultipleUsers dto, CancellationToken cancellationToken = default)
        {
            using var transaction = await _intranetContext.Database.BeginTransactionAsync(cancellationToken);
            var existingTeam = await _teamRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (existingTeam == null)
            {
                var team = new Team() 
                {
                    TeamName = dto.TeamName,
                    Clients = dto.Clients,
                    About = dto.About,
                    Company = dto.Company,
                    TechLead = dto.TechLead
                };
                _teamRepository.Create(team);
                await _teamRepository.SaveChangesAsync(cancellationToken);
                foreach (var user in dto.Members)
                {
                    var userTeam = new UserTeam()
                    {
                        TeamId = team.Id,
                        UserId = user.Id
                    };
                    _userTeamRepository.Create(userTeam);
                    await _userTeamRepository.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                await transaction.RollbackAsync(cancellationToken);
                return BadRequest();
            }
            await transaction.CommitAsync(cancellationToken);
            return Ok("Team successfully created !!!");
        }

        [HttpPut]
        public async Task<IActionResult> Update(TeamDTO dto, CancellationToken cancellationToken = default)
        {
            var team = await _teamRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (team is null) return NotFound();
            _mapper.Map(dto, team);

            await _teamRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var team = await _teamRepository.FindByIdAsync(id, cancellationToken);
            if (team is null) return NotFound();
            _teamRepository.Delete(team);
            await _teamRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
