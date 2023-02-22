namespace Intranet;

[Route("/api/[controller]/[action]")]
public class UserProjectController : BaseController
{
    public IMapper _mapper;
    public IUserProjectRepository _userProjectRepository;
    public IUserRepository _userRepository;
    public IProjectRepository _projectRepository;

    public UserProjectController(IMapper mapper,
                                 IUserProjectRepository userProjectRepository,
                                 IUserRepository userRepository,
                                 IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _userProjectRepository = userProjectRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var userProjects = await _userProjectRepository.FindAll().ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<UserProjectDTO>>(userProjects));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var userProject = await _userProjectRepository.FindByIdAsync(id, cancellationToken);
        if (userProject is null) return NotFound();
        return Ok(_mapper.Map<UserProjectDTO>(userProject));
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetProjectByUser(string userGuid, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindByGuidAsync(userGuid, cancellationToken);
        if (user is null) return NotFound();
        var userProjects = await _userProjectRepository.FindAll(ut => ut.User == user).ToListAsync();

        var projects = new List<Project>();
        foreach (var userProject in userProjects)
        {
            projects.Add(userProject.Project);
        }
        return Ok(_mapper.Map<IEnumerable<ProjectDTO>>(projects));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateUserProjectDTO dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindByGuidAsync(dto.UserGuid, cancellationToken);
        if (user is null || user.IsDisable == true) return NotFound();
        var project = await _projectRepository.FindByIdAsync(dto.ProjectId, cancellationToken);
        var userProject = new UserProject() { User = user, Project = project! };
        if (await _userProjectRepository.FindByUserId(user.Id, cancellationToken) != null)
        {
            var existingUserProject = await _userProjectRepository.FindAll(uf => uf.User.Id.Equals(dto.UserGuid)).FirstOrDefaultAsync();
            if (existingUserProject?.ProjectId == dto.ProjectId)
                return BadRequest("This user and project are already created !!");
            else _userProjectRepository.Create(userProject);
        }
        else _userProjectRepository.Create(userProject);
        await _userProjectRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { userProject.Id }, _mapper.Map<UserProjectDTO>(userProject));
    }
    [HttpPut]
    public async Task<IActionResult> Update(CreateUpdateUserProjectDTO dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindByGuidAsync(dto.UserGuid, cancellationToken);
        if (user is null) return NotFound();
        var userDTO = _mapper.Map<UserDTO>(user);
        var food = await _projectRepository.FindByIdAsync(dto.ProjectId, cancellationToken);
        if (food is null) return NotFound();
        var projectDTO = _mapper.Map<ProjectDTO>(food);
        var userProject = new UserProjectDTO() { User = userDTO, Project = projectDTO };
        _userProjectRepository.Update(_mapper.Map<UserProject>(userProject));
        await _userProjectRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var userProject = await _userProjectRepository.FindByIdAsync(id, cancellationToken);
        if (userProject is null) return NotFound();
        _userProjectRepository.Delete(userProject);
        await _userProjectRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
