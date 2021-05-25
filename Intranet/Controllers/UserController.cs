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
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(id, cancellationToken);
            if (user is null) return NotFound();
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin loginInfo, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByUserName(loginInfo.UserName, cancellationToken);
            if (user is null || user.IsDisable == true) return NotFound();
            if (loginInfo.Password != user.Password) return NotFound();
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO dto, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(dto);
            _userRepository.Create(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { user.Id }, _mapper.Map<UserDTO>(user));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (user is null) return NotFound();
            _mapper.Map<User>(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(id, cancellationToken);
            if (user is null) return NotFound();
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
