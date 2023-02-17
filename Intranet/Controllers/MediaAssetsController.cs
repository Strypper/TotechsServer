namespace Intranet;

[Route("api/[controller]/[action]")]
public class MediaAssetsController : BaseController
{
    public IMapper _mapper;
    public IMediaAssetsRepository _mediaAssetsRepository;

    public MediaAssetsController(IMapper mapper, IMediaAssetsRepository mediaAssetsRepository)
    {
        _mapper = mapper;
        _mediaAssetsRepository = mediaAssetsRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var mediaAssets = await _mediaAssetsRepository.FindAll()
            .ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<MediaAssetsDTO>>(mediaAssets));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var mediaAsset = await _mediaAssetsRepository.FindByIdAsync(id, cancellationToken);
        if (mediaAsset is null) return NotFound("");

        return Ok(_mapper.Map<MediaAssetsDTO>(mediaAsset));
    }


    [HttpPost]
    public async Task<IActionResult> Create(MediaAssetsDTO dto, CancellationToken cancellationToken = default)
    {
        var mediaAsset = _mapper.Map<MediaAssets>(dto);
        _mediaAssetsRepository.Create(mediaAsset);
        await _mediaAssetsRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { mediaAsset.Id }, _mapper.Map<MediaAssetsDTO>(mediaAsset));
    }


    [HttpPut]
    public async Task<IActionResult> Update(MediaAssetsDTO dto, CancellationToken cancellationToken = default)
    {
        var mediaAsset = await _mediaAssetsRepository.FindByIdAsync(dto.Id, cancellationToken);
        if (mediaAsset is null) return NotFound("");

        _mapper.Map(dto, mediaAsset);
        _mediaAssetsRepository.Update(mediaAsset);
        await _mediaAssetsRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var mediaAsset = await _mediaAssetsRepository.FindByIdAsync(id, cancellationToken);
        if (mediaAsset is null) return NotFound("");
        _mediaAssetsRepository.Delete(mediaAsset);
        await _mediaAssetsRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken = default)
    {
        await _mediaAssetsRepository.DeleteAll(cancellationToken);
        await _mediaAssetsRepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
