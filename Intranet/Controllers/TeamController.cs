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
