using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Intranet.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserFoodController : BaseController
    {
        public IMapper _mapper;
        public IUserFoodRepository _userFoodRepository;
        public IUserRepository _userRepository;
        public IFoodRepository _foodRepository;

        public UserFoodController(IMapper mapper, IUserFoodRepository userFoodRepository, IUserRepository userRepository, IFoodRepository foodRepository)
        {
            _mapper = mapper;
            _userFoodRepository = userFoodRepository;
            _userRepository = userRepository;
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var userFoods = await _userFoodRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<UserFoodDTO>>(userFoods));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var userFood = await _userFoodRepository.FindByIdAsync(id, cancellationToken);
            if (userFood is null) return NotFound();
            return Ok(_mapper.Map<UserFoodDTO>(userFood));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserSelectedFood(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user is null) return NotFound();
            var userSelectedFood = await _userFoodRepository.FindByUserId(userId);
            return Ok(_mapper.Map<UserFoodDTO>(userSelectedFood));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserFoodDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(dto.UserId, cancellationToken);
            var userDTO = _mapper.Map<UserDTO>(user);
            var food = await _foodRepository.FindByIdAsync(dto.FoodId, cancellationToken);
            var foodDTO = _mapper.Map<FoodDTO>(food);
            var userFood = new UserFoodDTO() { User = userDTO, Food = foodDTO};
            _userFoodRepository.Create(_mapper.Map<UserFood>(userFood));
            await _userFoodRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { userFood.Id }, _mapper.Map<UserFoodDTO>(userFood));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserFoodDTO dto, CancellationToken cancellationToken = default)
        {
            var userFood = await _userFoodRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (userFood is null) return NotFound();
            _mapper.Map(dto, userFood);
            await _userFoodRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var userFood = await _userFoodRepository.FindByIdAsync(id, cancellationToken);
            if (userFood is null) return NotFound();
            _userFoodRepository.Delete(userFood);
            await _userFoodRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
