using Intranet.Contract;
using Intranet.Entities;
using Intranet.Entities.Entities;

namespace Intranet.Repo
{
    public class MeetingRepository : BaseRepository<MeetingSchedule>, IMeetingRepository
    {
        public MeetingRepository(IntranetContext ic) : base(ic) { }
    }
}
