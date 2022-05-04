using AutoMapper;
using Intranet.Constants;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Intranet.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Intranet.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class FoodController : BaseController
    {
        public IMapper _mapper;
        public IFoodRepository _foodRepository;
        public IUserFoodRepository _userFoodRepository;
        public IWebHostEnvironment _webHostEnvironment;
        private NotificationHubClient _hub;
        public FoodController(IMapper mapper, 
                              IFoodRepository foodRepository, 
                              IUserFoodRepository userFoodRepository,
                              IWebHostEnvironment webHostEnvironment)
        {
            _hub = Notifications.Instance.Hub;
            _mapper = mapper;
            _foodRepository = foodRepository;
            _userFoodRepository = userFoodRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var foods = await _foodRepository.FindAll().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<FoodDTO>>(foods));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var food = await _foodRepository.FindByIdAsync(id, cancellationToken);
            if (food is null) return NotFound();
            return Ok(_mapper.Map<FoodDTO>(food));
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodDTO dto, CancellationToken cancellationToken = default)
        {
            var food = _mapper.Map<Food>(dto);
            _foodRepository.Create(food);
            await _foodRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(Get), new { food.Id }, _mapper.Map<FoodDTO>(food));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMultipleFood(IEnumerable<FoodDTO> dtos, CancellationToken cancellationToken = default)
        {
            var foods = _mapper.Map<IEnumerable<Food>>(dtos);
            foreach (var food in foods)
            {
                _foodRepository.Create(food);
            }
            await _foodRepository.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<FoodDTO>>(foods));
        }

        [HttpPut]
        public async Task<IActionResult> Update(FoodDTO dto, CancellationToken cancellationToken = default)
        {
            var food = await _foodRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (food is null) return NotFound();
            _mapper.Map(dto, food);

            //Remove all users choose this food if unavaible is toggled
            if(dto.IsUnavailable == true)
            {
                var usersChooseThisFood = await _userFoodRepository.FindAll(uf => uf.FoodId == dto.Id).ToListAsync();
                foreach (var userFood in usersChooseThisFood)
                {
                    _userFoodRepository.Delete(userFood);
                }
                await _userFoodRepository.SaveChangesAsync(cancellationToken);
            }

            await _foodRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var food = await _foodRepository.FindByIdAsync(id, cancellationToken);
            if (food is null) return NotFound();
            _foodRepository.Delete(food);
            await _foodRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken = default)
        {
            await _foodRepository.DeleteAll(cancellationToken);
            await _foodRepository.SaveChangesAsync(cancellationToken);
            return NoContent();
        }
    }
}
