namespace Intranet;

[Route("/api/[controller]/[action]")]
public class FoodController : BaseController
{
    #region [Services]
    public IMapper _mapper;
    public IFoodRepository _foodRepository;
    public IUserFoodRepository _userFoodRepository;
    //private NotificationHubClient _hub;
    #endregion

    #region [CTor]
    public FoodController(IMapper mapper,
                          IFoodRepository foodRepository,
                          IUserFoodRepository userFoodRepository)
    {
        //_hub = Notifications.Instance.Hub;
        _mapper = mapper;
        _foodRepository = foodRepository;
        _userFoodRepository = userFoodRepository;
    }
    #endregion

    #region [GET]

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
    #endregion

    #region [POST]

    [HttpPost]
    public async Task<IActionResult> Create(FoodDTO dto, CancellationToken cancellationToken = default)
    {
        var food = _mapper.Map<Food>(dto);
        await _foodRepository.CreateAsync(food, cancellationToken);
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
    #endregion

    #region [PUT]

    [HttpPut]
    public async Task<IActionResult> Update(FoodDTO dto, CancellationToken cancellationToken = default)
    {
        var food = await _foodRepository.FindByIdAsync(dto.Id, cancellationToken);
        if (food is null) return NotFound();
        _mapper.Map(dto, food);

        //Remove all users choose this food if unavaible is toggled
        if (dto.IsUnavailable == true)
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
    #endregion

    #region [DELETE]

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

    #endregion
}
