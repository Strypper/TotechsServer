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
    public class CertificationController : BaseController
    {
        public IMapper _mapper;
        public ICertificationRepository _certificationRepository;

        public CertificationController(IMapper mapper, ICertificationRepository certificationRepository)
        {
            _mapper = mapper;
            _certificationRepository = certificationRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var certifications = await _certificationRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<CertificationDTO>>(certifications));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var certification = await _certificationRepository.FindAll(c => c.Id == id)
                                                            .FirstOrDefaultAsync(cancellationToken);
            if (certification is null) return NotFound();
            return Ok(_mapper.Map<CertificationDTO>(certification));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CertificationDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var certification = _mapper.Map<Certification>(dto);

            _certificationRepository.Create(certification);
            await _certificationRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { certification.Id }, _mapper.Map<CertificationDTO>(certification));
        }


        [HttpPut]
        public async Task<IActionResult> Update(CertificationDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var certification = await _certificationRepository.FindByIdAsync(dto.Id, cancellationToken);
            await _certificationRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var certification = await _certificationRepository.FindByIdAsync(id, cancellationToken);
            if (certification is null) return NotFound();
            _certificationRepository.Delete(certification);
            await _certificationRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
