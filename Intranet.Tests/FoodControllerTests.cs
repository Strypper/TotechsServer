using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Tests;

public class FoodControllerTests
{
    #region [ Services ]
    private readonly IMapper _mapper;
    private readonly IFoodRepository _foodRepository;
    private readonly IUserFoodRepository _userFoodRepository;
    #endregion

    #region [ CTor ]
    public FoodControllerTests()
    {
        _mapper = A.Fake<IMapper>();
        _foodRepository = A.Fake<IFoodRepository>();
        _userFoodRepository = A.Fake<IUserFoodRepository>();
    }
    #endregion

    #region [Tests]
    [Fact]
    public async Task FoodController_GetAll_ReturnOk()
    {
        #region[ Arrange ]
        var foods = A.Fake<IEFMock<Food>>();
        var foodsDTO = A.Fake<IEnumerable<FoodDTO>>();

        A.CallTo(() => _foodRepository.FindAll(null)).Returns(foods);
        A.CallTo(() => _mapper.Map<IEnumerable<FoodDTO>>(foods)).Returns(foodsDTO);

        var controller = new FoodController(_mapper, _foodRepository, _userFoodRepository);
        #endregion

        #region[ Act ]
        var result = await controller.GetAll();
        #endregion

        #region[ Assert ]
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        #endregion
    }

    [Fact]
    public async Task FoodController_Get_ReturnNotFound()
    {
        #region [ Arrange ]
        int id = 0;

        A.CallTo(() => _foodRepository.FindByIdAsync(id, CancellationToken.None))
                                      .Returns(Task.FromResult<Food?>(null)); ;

        var controller = new FoodController(_mapper, _foodRepository, _userFoodRepository);
        #endregion

        #region [ Act ]
        var result = await controller.Get(id);
        #endregion

        #region [ Assert ]
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundResult));
        #endregion
    }

    [Fact]
    public async Task FoodController_CreateFood_ReturnCreatedAtActionResult()
    {
        #region[ Arrange ]
        var food = A.Fake<Food>();
        var foodDTO = A.Fake<FoodDTO>();

        A.CallTo(() => _mapper.Map<Food>(foodDTO)).Returns(food);
        A.CallTo(() => _foodRepository.CreateAsync(food, CancellationToken.None));

        var controller = new FoodController(_mapper, _foodRepository, _userFoodRepository);
        #endregion

        #region[ Act ]
        var result = await controller.Create(foodDTO);
        #endregion

        #region[ Assert ]
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedAtActionResult>();
        #endregion
    }
    #endregion

}