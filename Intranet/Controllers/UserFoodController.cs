namespace Intranet;

[Route("/api/[controller]/[action]")]
public class UserFoodController : BaseController
{
    public IMapper _mapper;
    public IUserFoodRepository _userFoodRepository;
    public IUserRepository _userRepository;
    public IFoodRepository _foodRepository;

    public UserFoodController(IMapper mapper,
                              IUserFoodRepository userFoodRepository,
                              IUserRepository userRepository,
                              IFoodRepository foodRepository)
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
    public async Task<IActionResult> GetUserSelectedFood(string userGuid, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindByGuidAsync(userGuid, cancellationToken);
        if (user is null) return NotFound();
        var userSelectedFood = await _userFoodRepository.FindByUserId(userGuid, cancellationToken);
        return Ok(_mapper.Map<UserFoodDTO>(userSelectedFood));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateUserFoodDTO dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindByGuidAsync(dto.UserGuid, cancellationToken);
        if (user is null || user.IsDisable == true) return NotFound();
        var food = await _foodRepository.FindByIdAsync(dto.FoodId, cancellationToken);
        if (food is null || food.IsUnavailable == true) return NotFound();
        var userFood = new UserFood() { User = user, Food = food };
        if (await _userFoodRepository.FindByUserId(user.Id, cancellationToken) != null)
        {
            var existingUserFood = await _userFoodRepository.FindAll(uf => uf.User.Id.Equals(dto.UserGuid)).FirstOrDefaultAsync();
            if (existingUserFood is not null)
            {
                existingUserFood.FoodId = dto.FoodId;
                _userFoodRepository.Update(existingUserFood);
            }
        }
        else _userFoodRepository.Create(userFood);
        await _userFoodRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { userFood.Id }, _mapper.Map<UserFoodDTO>(userFood));
    }
    [HttpPut]
    public async Task<IActionResult> Update(CreateUpdateUserFoodDTO dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindByGuidAsync(dto.UserGuid, cancellationToken);
        if (user is null) return NotFound();
        var userDTO = _mapper.Map<UserDTO>(user);
        var food = await _foodRepository.FindByIdAsync(dto.FoodId, cancellationToken);
        if (food is null) return NotFound();
        var foodDTO = _mapper.Map<FoodDTO>(food);
        var userFood = new UserFoodDTO() { User = userDTO, Food = foodDTO };
        _userFoodRepository.Update(_mapper.Map<UserFood>(userFood));
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
