using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Intranet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpertiseController : BaseController
    {
        public IMapper _mapper;
        public IExpertiseRepository _expertiseRepository;
        public UserManager<User> _userManager;

        public ExpertiseController(IMapper mapper, IExpertiseRepository expertiseRepository, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _expertiseRepository = expertiseRepository;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var expertises = await _expertiseRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ExpertiseDTO>>(expertises));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var expertise = await _expertiseRepository.FindAll(c => c.Id == id)
                                                            .FirstOrDefaultAsync(cancellationToken);
            if (expertise is null) return NotFound();
            return Ok(_mapper.Map<ExpertiseDTO>(expertise));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) return NotFound($"No User With Id {userId} Found!");

            var expertises = await _expertiseRepository.FindAll().Where(x => x.UserExpertises.Select(y => userId.Equals(y.UserId)).Any()).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<Expertise>>(expertises));
        }


        [HttpPost]
        public async Task<IActionResult> Create(ExpertiseDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var expertise = _mapper.Map<Expertise>(dto);

            _expertiseRepository.Create(expertise);
            await _expertiseRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { expertise.Id }, _mapper.Map<ExpertiseDTO>(expertise));
        }


        [HttpPut]
        public async Task<IActionResult> Update(ExpertiseDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var expertise = await _expertiseRepository.FindByIdAsync(dto.Id, cancellationToken);
            // update here
            await _expertiseRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var expertise = await _expertiseRepository.FindByIdAsync(id, cancellationToken);
            if (expertise is null) return NotFound();
            _expertiseRepository.Delete(expertise);
            await _expertiseRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
