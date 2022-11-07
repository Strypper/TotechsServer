using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterestController : BaseController
    {
        public IMapper _mapper;
        public IInterestRepository _interestRepository;

        public InterestController(IMapper mapper, IInterestRepository interestRepository)
        {
            _mapper = mapper;
            _interestRepository = interestRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var interests = await _interestRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<InterestDTO>>(interests));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var interest = await _interestRepository.FindByIdAsync(id, cancellationToken);
            if (interest is null) return NotFound();
            return Ok(_mapper.Map<InterestDTO>(interest));
        }


        [HttpPost]
        public async Task<IActionResult> Create(InterestDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var interest = _mapper.Map<Interest>(dto);

            _interestRepository.Create(interest);
            await _interestRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { interest.Id }, _mapper.Map<InterestDTO>(interest));
        }


        [HttpPut]
        public async Task<IActionResult> Update(InterestDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var interest = await _interestRepository.FindByIdAsync(dto.Id, cancellationToken);
            // update here
            await _interestRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var interest = await _interestRepository.FindByIdAsync(id, cancellationToken);
            if (interest is null) return NotFound();
            _interestRepository.Delete(interest);
            await _interestRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
