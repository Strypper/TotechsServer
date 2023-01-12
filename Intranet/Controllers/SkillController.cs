using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class SkillController : BaseController
    {
        private IMapper                 _mapper;
        private IUserRepository         _userRepository;
        private ISkillRepository _skillRepository;
        private IExpertiseRepository _expertiseRepository;
        private UserManager<User> _userManager;
        public SkillController(IMapper mapper,
                                      IUserRepository userRepository,
                                      ISkillRepository skillRepository,
                                      IExpertiseRepository expertiseRepository,
                                      UserManager<User> userManager)
        {
            _mapper                 = mapper;
            _userRepository         = userRepository;
            _skillRepository = skillRepository;
            _expertiseRepository = expertiseRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var skills = await _skillRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<SkillDTO>>(skills));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var skill = await _skillRepository.FindAll(c => c.Id == id)
                                                            .FirstOrDefaultAsync(cancellationToken);
            if (skill is null) return NotFound();
            return Ok(_mapper.Map<SkillDTO>(skill));
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
        public async Task<IActionResult> Create(SkillDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var skill = _mapper.Map<Skill>(dto);
            skill.SkillExpertises = new List<SkillExpertise>();
            foreach(var se in dto.SkillExpertises)
            {
                var expertise = await _expertiseRepository.FindByIdAsync(se.ExpertiseId, cancellationToken);
                skill.SkillExpertises.Add(new SkillExpertise
                {
                    Expertise = expertise,
                    Details = se.Details,
                    Value = se.Value
                });
            }
            
            _skillRepository.Create(skill);
            await _skillRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { skill.Id }, _mapper.Map<SkillDTO>(skill));
        }


        [HttpPut]
        public async Task<IActionResult> Update(SkillDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest($"{nameof(dto)} was null");

            var skill = await _skillRepository.FindByIdAsync(dto.Id, cancellationToken);
            await _skillRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var skill = await _skillRepository.FindByIdAsync(id, cancellationToken);
            if (skill is null) return NotFound();
            _skillRepository.Delete(skill);
            await _skillRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
