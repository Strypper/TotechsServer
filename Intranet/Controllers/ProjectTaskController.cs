namespace Intranet;

[Route("api/[controller]/[action]")]
public class ProjectTaskController : BaseController
{
    public IMapper _mapper;
    public IProjectTaskRepository _projectTaskRepository;

    public ProjectTaskController(IMapper mapper, IProjectTaskRepository projectTaskRepository)
    {
        _mapper = mapper;
        _projectTaskRepository = projectTaskRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var projectTasks = await _projectTaskRepository.FindAll()
            .ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ProjectTaskDTO>>(projectTasks));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var projectTask = await _projectTaskRepository.FindByIdAsync(id, cancellationToken);
        if (projectTask is null) return NotFound("");

        return Ok(_mapper.Map<ProjectTaskDTO>(projectTask));
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProjectTaskDTO dto, CancellationToken cancellationToken = default)
    {
        var projectTask = _mapper.Map<ProjectTask>(dto);
        _projectTaskRepository.Create(projectTask);
        await _projectTaskRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { projectTask.Id }, _mapper.Map<ProjectTaskDTO>(projectTask));
    }


    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskDTO dto, CancellationToken cancellationToken = default)
    {
        var projectTask = await _projectTaskRepository.FindByIdAsync(dto.Id, cancellationToken);
        if (projectTask is null) return NotFound("");

        _mapper.Map(dto, projectTask);
        _projectTaskRepository.Update(projectTask);
        await _projectTaskRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var projectTask = await _projectTaskRepository.FindByIdAsync(id, cancellationToken);
        if (projectTask is null) return NotFound("");
        _projectTaskRepository.Delete(projectTask);
        await _projectTaskRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken = default)
    {
        await _projectTaskRepository.DeleteAll(cancellationToken);
        await _projectTaskRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
