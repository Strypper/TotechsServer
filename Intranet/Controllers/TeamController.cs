using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public ITeamRepository _teamRepository;
        public IntranetContext _intranetContext { get; set; }
        public TeamController(IMapper mapper,
                              IUserTeamRepository userTeamRepository,
                              ITeamRepository teamRepository,
                              IntranetContext intranetContext)
        {
            _mapper = mapper;
            _userTeamRepository = userTeamRepository;
            _teamRepository = teamRepository;
            _intranetContext = intranetContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var teams = await _teamRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<TeamDTO>>(teams));
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
            var existingTeam = await _teamRepository.FindByIdAsync(dto.Team.Id, cancellationToken);
            if (existingTeam == null)
            {
                var team = _mapper.Map<Team>(dto.Team);
                _teamRepository.Create(team);
                await _teamRepository.SaveChangesAsync(cancellationToken);
                foreach (var user in dto.Users)
                {
                    var userTeam = new UserTeam()
                    {
                        Team = team,
                        User = _mapper.Map<User>(user)
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
