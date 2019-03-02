using KMeeting.DAL;

namespace KMeeting.Repository
{
    public abstract class RepositoryBase
    {
        protected MeetingContext Context { get; set; }

        protected RepositoryBase(MeetingContext context)
        {
            Context = context;
        }
    }
}