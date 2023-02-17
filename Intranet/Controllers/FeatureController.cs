namespace Intranet;

[Route("api/[controller]/[action]")]
public class FeatureController : BaseController
{
    public IMapper _mapper;
    public IFeatureRepository _featureRepository;

    public FeatureController(IMapper mapper, IFeatureRepository featureRepository)
    {
        _mapper = mapper;
        _featureRepository = featureRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var features = await _featureRepository.FindAll()
            .Include(fea => fea.MediaAssests)
            .Include(fea => fea.SubFeatures)
            .ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<FeatureDTO>>(features));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var feature = await _featureRepository.FindAll(fea => fea.Id == id)
            .Include(fea => fea.MediaAssests)
            .Include(fea => fea.SubFeatures)
            .FirstOrDefaultAsync(cancellationToken);
        if (feature is null) return NotFound("");

        return Ok(_mapper.Map<FeatureDTO>(feature));
    }


    [HttpPost]
    public async Task<IActionResult> Create(FeatureDTO dto, CancellationToken cancellationToken = default)
    {
        var feature = _mapper.Map<Feature>(dto);
        _featureRepository.Create(feature);
        await _featureRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { feature.Id }, _mapper.Map<FeatureDTO>(feature));
    }


    [HttpPut]
    public async Task<IActionResult> Update(FeatureDTO dto, CancellationToken cancellationToken = default)
    {
        var feature = await _featureRepository.FindByIdAsync(dto.Id, cancellationToken);
        if (feature is null) return NotFound("");

        _mapper.Map(dto, feature);
        _featureRepository.Update(feature);
        await _featureRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var feature = await _featureRepository.FindByIdAsync(id, cancellationToken);
        if (feature is null) return NotFound("");
        _featureRepository.Delete(feature);
        await _featureRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken = default)
    {
        await _featureRepository.DeleteAll(cancellationToken);
        await _featureRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
