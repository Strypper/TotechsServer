using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System.Linq.Expressions;

namespace Intranet.Tests;

public class ContributionControllerTest
{
    private readonly Fixture _fixture;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IContributionRepository> _mockContribution;
    private readonly Mock<IUserRepository> _mockUser;

    #region [ CTor ]
    public ContributionControllerTest()
    {
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _fixture = fixture;
        _mockMapper = new();
        _mockContribution = new();
        _mockUser = new();
    }
    #endregion [ CTor ]

    #region [ Methods ]
    [Fact]
    public async Task GetAll_ShouldReturn200Ok()
    {
        // Arrange
        var contributions = new List<Contribution>();
        var mockQueryable = contributions.BuildMock();  // handle include()

        var contributionDTOs = new List<ContributionDTO>();

        _mockMapper.Setup(x => x.Map<IEnumerable<ContributionDTO>>(It.IsAny<IEnumerable<Contribution>>())).Returns(contributionDTOs);

        _mockContribution.Setup(x => x.FindAll(It.IsAny<Expression<Func<Contribution, bool>>?>())).Returns(mockQueryable);
        var controller = new ContributionController(_mockMapper.Object, _mockUser.Object, _mockContribution.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        _mockContribution.Verify(x => x.FindAll(It.IsAny<Expression<Func<Contribution, bool>>?>()), Moq.Times.Once);    // Not beneficial in this case but will
                                                                                                                        // be good practice for some methods
                                                                                                                        // like Add(), SaveChanges()
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        var actualValue = okResult!.Value as IEnumerable<ContributionDTO>;
        actualValue.Should().BeEquivalentTo(contributionDTOs);
    }

    [Fact]
    public async Task Get_ShouldReturn404NotFound()
    {
        var contributions = new List<Contribution>();
        var mock = contributions.BuildMock();

        var contributionDTO = _fixture.Create<ContributionDTO>();

        _mockMapper.Setup(x => x.Map<ContributionDTO>(It.IsAny<Contribution>())).Returns(contributionDTO);

        _mockContribution.Setup(x => x.FindAll(It.IsAny<Expression<Func<Contribution, bool>>?>())).Returns(mock);
        var controller = new ContributionController(_mockMapper.Object, _mockUser.Object, _mockContribution.Object);

        var result = await controller.Get(0);

        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Get_ShouldReturn200Ok()
    {
        var contributions = new List<Contribution>()
        {
            new Contribution()
        };
        var mock = contributions.BuildMock();
        var contributionDTO = _fixture.Create<ContributionDTO>();

        _mockMapper.Setup(x => x.Map<ContributionDTO>(It.IsAny<Contribution>())).Returns(contributionDTO);

        _mockContribution.Setup(x => x.FindAll(It.IsAny<Expression<Func<Contribution, bool>>?>())).Returns(mock);

        var controller = new ContributionController(_mockMapper.Object, _mockUser.Object, _mockContribution.Object);

        var result = await controller.Get(1);

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        var actualValue = okResult!.Value as ContributionDTO;
        actualValue.Should().BeEquivalentTo(contributionDTO);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var contribution = _fixture.Create<Contribution>();
        var contributionDTO = _fixture.Create<ContributionDTO>();

        _mockUser.Setup(x => x.FindByGuidAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(default(User)));

        _mockMapper.Setup(x => x.Map<Contribution>(It.IsAny<ContributionDTO>())).Returns(contribution);
        _mockMapper.Setup(x => x.Map<ContributionDTO>(It.IsAny<Contribution>())).Returns(contributionDTO);

        _mockContribution.Setup(x => x.Create(It.IsAny<Contribution>()));
        _mockContribution.Setup(x => x.SaveChangesAsync(default));

        var controller = new ContributionController(_mockMapper.Object, _mockUser.Object, _mockContribution.Object);

        // Act
        var result = await controller.Create(contributionDTO);

        // Assert
        _mockContribution.Verify(x => x.Create(It.IsAny<Contribution>()), Moq.Times.Once);
        _mockContribution.Verify(x => x.SaveChangesAsync(default), Moq.Times.Once);

        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    //[Fact]
    //public async Task Create_ShouldReturnNotFound()
    //{ }

    [Fact]
    public async Task UpdateApprove_ShouldReturnNoContent()
    {
        // Arrange
        Contribution? contribution = _fixture.Create<Contribution>();
        contribution.IsApproved = false;

        var contributionDTO = _fixture.Create<ContributionDTO>();

        _mockContribution.Setup(x => x.FindByIdAsync(It.IsAny<int>(), default)).Returns(Task.FromResult<Contribution?>(contribution));

        _mockContribution.Setup(x => x.SaveChangesAsync(default));

        var controller = new ContributionController(_mockMapper.Object, _mockUser.Object, _mockContribution.Object);

        // Act
        var result = await controller.UpdateApprove(contributionDTO);

        // Assert
        _mockContribution.Verify(x => x.SaveChangesAsync(default), Moq.Times.Once);
        contribution.IsApproved.Should().BeTrue();

        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }

    //[Fact]
    //public async Task UpdateApprove_ShouldReturnNotFound()
    //{ }
    #endregion
}
