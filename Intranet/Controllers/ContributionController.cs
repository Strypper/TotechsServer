namespace Intranet;

[Route("/api/[controller]/[action]")]
public class ContributionController : BaseController
{
    public IMapper _mapper;
    public IUserRepository _userRepository;
    public IContributionRepository _contributionRepository;
    public ContributionController(IMapper mapper,
                                  IUserRepository userRepository,
                                  IContributionRepository contributionRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _contributionRepository = contributionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var contributions = await _contributionRepository.FindAll().Include(contribution => contribution.Contributor).ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<ContributionDTO>>(contributions));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var contribution = await _contributionRepository.FindAll(c => c.Id == id)
                                                        .Include(contribution => contribution.Contributor)
                                                        .FirstOrDefaultAsync(cancellationToken);
        if (contribution is null) return NotFound();
        return Ok(_mapper.Map<ContributionDTO>(contribution));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ContributionDTO dto, CancellationToken cancellationToken = default)
    {
        var contribution = _mapper.Map<Contribution>(dto);
        var user = await _userRepository.FindByGuidAsync(contribution.Contributor.Id, cancellationToken);
        contribution.Contributor = user!;
        _contributionRepository.Create(contribution);
        await _contributionRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { contribution.Id }, _mapper.Map<ContributionDTO>(contribution));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateApprove(ContributionDTO dto, CancellationToken cancellationToken = default)
    {
        var contribution = await _contributionRepository.FindByIdAsync(dto.Id, cancellationToken);
        contribution!.IsApproved = true;
        await _contributionRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var contribution = await _contributionRepository.FindByIdAsync(id, cancellationToken);
        if (contribution is null) return NotFound();
        _contributionRepository.Delete(contribution);
        await _contributionRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken = default)
    {
        await _contributionRepository.DeleteAll(cancellationToken);
        await _contributionRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
