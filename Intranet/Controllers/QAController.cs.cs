namespace Intranet;

public class QAController : BaseController
{
    #region [Service]
    public IMapper _mapper;
    public IQARepository _qARepository;
    public IUserQARepository _userQARepository;
    #endregion

    #region [CTor]
    public QAController(IMapper mapper,
                          IQARepository qARepository,
                          IUserQARepository userQARepository)
    {
        //_hub = Notifications.Instance.Hub;
        _mapper = mapper;
        _qARepository = qARepository;
        _userQARepository = userQARepository;
    }
    #endregion

    #region [GET]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var qAs = await _qARepository.FindAll().ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<QADTO>>(qAs));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var qA = await _qARepository.FindByIdAsync(id, cancellationToken);
        if (qA is null) return NotFound();
        return Ok(_mapper.Map<QADTO>(qA));
    }
    #endregion

    #region [POST]

    [HttpPost]
    public async Task<IActionResult> Create(QADTO dto, CancellationToken cancellationToken = default)
    {
        var qA = _mapper.Map<QA>(dto);
        await _qARepository.CreateAsync(qA, cancellationToken);
        await _qARepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { qA.Id }, _mapper.Map<QADTO>(qA));
    }
    #endregion

    #region [PUT]
    [HttpPut]
    public async Task<IActionResult> Update(QADTO dto, CancellationToken cancellationToken = default)
    {
        var qA = await _qARepository.FindByIdAsync(dto.Id, cancellationToken);
        if (qA is null) return NotFound();
        _mapper.Map(dto, qA);

        await _qARepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion

    #region [DELETE]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var qA = await _qARepository.FindByIdAsync(id, cancellationToken);
        if (qA is null) return NotFound();
        _qARepository.Delete(qA);
        await _qARepository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
    #endregion
}
