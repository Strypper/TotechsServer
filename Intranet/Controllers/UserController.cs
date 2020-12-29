using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserController : BaseController
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var foods = await _userRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(foods));
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (user is null) return NotFound();
            _mapper.Map<User>(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
