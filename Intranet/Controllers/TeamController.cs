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
    public class TeamController : BaseController
    {
        public IMapper _mapper;
        public ITeamRepository _teamRepository;
        public TeamController(IMapper mapper, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var Teams = await _teamRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<TeamDTO>>(Teams));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var Team = await _teamRepository.FindByIdAsync(id, cancellationToken);
            if (Team is null) return NotFound();
            return Ok(_mapper.Map<TeamDTO>(Team));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamDTO dto, CancellationToken cancellationToken = default)
        {
            var Team = _mapper.Map<Team>(dto);
            _teamRepository.Create(Team);
            await _teamRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { Team.Id }, _mapper.Map<TeamDTO>(Team));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TeamDTO dto, CancellationToken cancellationToken = default)
        {
            var Team = await _teamRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (Team is null) return NotFound();
            _mapper.Map(dto, Team);

            await _teamRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var Team = await _teamRepository.FindByIdAsync(id, cancellationToken);
            if (Team is null) return NotFound();
            _teamRepository.Delete(Team);
            await _teamRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
