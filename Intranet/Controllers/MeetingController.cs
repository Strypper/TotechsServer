using Intranet.Constants;
using Intranet.Helpers;
using Microsoft.Azure.NotificationHubs;
using System.Xml.Linq;

namespace Intranet;

[Route("/api/[controller]/[action]")]
public class MeetingController : BaseController
{
    public IMapper _mapper;
    public IMeetingRepository _meetingRepository;
    private NotificationHubClient _hub;
    public MeetingController(IMapper mapper,
                             IMeetingRepository meetingRepository)
    {
        _mapper = mapper;
        _meetingRepository = meetingRepository;
        _hub = Notifications.Instance.Hub;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var meetings = await _meetingRepository.FindAll().ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<MeetingScheduleDTO>>(meetings));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        var meeting = await _meetingRepository.FindByIdAsync(id, cancellationToken);
        if (meeting is null) return NotFound();
        return Ok(_mapper.Map<MeetingScheduleDTO>(meeting));
    }

    [HttpPost]
    public async Task<IActionResult> Create(MeetingScheduleDTO dto, CancellationToken cancellationToken = default)
    {
        var meeting = _mapper.Map<MeetingSchedule>(dto);
        _meetingRepository.Create(meeting);
        await _meetingRepository.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { meeting.Id }, _mapper.Map<MeetingScheduleDTO>(meeting));
    }

    [HttpPost]
    public async Task<IActionResult> SendNotificationToAllMembers(CancellationToken cancellationToken = default)
    {
        var payload = new string[] { XMLConstant.MeetingToastXML, XMLConstant.MeetingLiveTitleXML };
        foreach (string xml in payload)
        {
            var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), xml);
            XDocument xmlDocument = XDocument.Load(xmlPath);
            await _hub.SendWindowsNativeNotificationAsync(xmlDocument.ToString(), cancellationToken);
        }
        return NoContent();
    }
}
