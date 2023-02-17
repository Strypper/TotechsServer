using Intranet.Contract;
using Intranet.Entities;

namespace Intranet.Repo;

public class MeetingRepository : BaseRepository<MeetingSchedule>, IMeetingRepository
{
    public MeetingRepository(IntranetContext ic) : base(ic) { }
}
