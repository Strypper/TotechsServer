namespace Intranet;

[Route("/api/[controller]/[action]")]
public class ProjectController : BaseController
{
    public IMapper _mapper;
    public IUserProjectRepository _userProjectRepository;
    public IUserRepository _userRepository;
    public IProjectRepository _projectRepository;
    public IntranetContext _intranetContext { get; set; }
    public ProjectController(IMapper mapper,
                              IUserProjectRepository userProjectRepository,
                              IUserRepository userRepository,
                              IProjectRepository projectRepository,
                              IntranetContext intranetContext)
    {
        _mapper = mapper;
        _userProjectRepository = userProjectRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _intranetContext = intranetContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var projects = await _projectRepository.FindAll().ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ProjectDTO>>(projects));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjectsWithMembers(CancellationToken cancellationToken = default)
    {
        var projects = await _projectRepository.FindAll().ToListAsync(cancellationToken);
        var projectsDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        var projectsWithUserDTO = new List<ProjectWithMemberDTO>();
        foreach (var projectDTO in projectsDTO)
        {
            var userProjectDTO = await _userProjectRepository
                                    .FindAll(ut => ut.ProjectId == projectDTO.Id)
                                    .ToListAsync();
            var members = userProjectDTO.Where(ut => ut.Project.Id == projectDTO.Id)
                                     .Select(ut => ut.User);
            var projectsWithMembers = new ProjectWithMemberDTO()
            {
                Id = projectDTO.Id,
                ProjectName = projectDTO.ProjectName,
                Clients = projectDTO.Clients,
                About = projectDTO.About,
                TechLead = projectDTO.TechLead,
                ProjectLogo = projectDTO.ProjectLogo,
                ProjectBackground = projectDTO.ProjectBackground,
                MicrosoftStoreLink = projectDTO.MicrosoftStoreLink,
                StartTime = projectDTO.StartTime,
                Deadline = projectDTO.Deadline,
                Members = _mapper.Map<IEnumerable<UserDTO>>(members)
            };
            projectsWithUserDTO.Add(projectsWithMembers);
        }
        return Ok(projectsWithUserDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.FindByIdAsync(id, cancellationToken);
        if (project is null) return NotFound();
        return Ok(_mapper.Map<ProjectDTO>(project));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectDTO dto, CancellationToken cancellationToken = default)
    {
        var project = _mapper.Map<Project>(dto);
        _projectRepository.Create(project);
        await _projectRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { project.Id }, _mapper.Map<ProjectDTO>(project));
    }

    [HttpPost]
    public async Task<IActionResult> CreatProjectWithUsers(CreateProjectWithMultipleUsers dto, CancellationToken cancellationToken = default)
    {
        using var transaction = await _intranetContext.Database.BeginTransactionAsync(cancellationToken);
        var existingProject = await _projectRepository.FindByIdAsync(dto.Id, cancellationToken);
        if (existingProject == null)
        {
            var project = new Project()
            {
                ProjectName = dto.ProjectName,
                Clients = dto.Clients,
                About = dto.About,
                TechLead = dto.TechLead
            };
            _projectRepository.Create(project);
            await _projectRepository.SaveChangesAsync(cancellationToken);
            foreach (var user in dto.Members)
            {
                var userProject = new UserProject()
                {
                    ProjectId = project.Id,
                    UserId = user.Guid
                };
                _userProjectRepository.Create(userProject);
                await _userProjectRepository.SaveChangesAsync(cancellationToken);
            }
        }
        else
        {
            await transaction.RollbackAsync(cancellationToken);
            return BadRequest();
        }
        await transaction.CommitAsync(cancellationToken);
        return Ok("Project successfully created !!!");
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectDTO dto, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.FindByIdAsync(dto.Id, cancellationToken);
        if (project is null) return NotFound();
        _mapper.Map(dto, project);

        await _projectRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.FindByIdAsync(id, cancellationToken);
        if (project is null) return NotFound();
        _projectRepository.Delete(project);
        await _projectRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
