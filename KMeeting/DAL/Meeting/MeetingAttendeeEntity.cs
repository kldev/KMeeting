namespace KMeeting.DAL.Meeting
{
    public class MeetingAttendeeEntity : BaseEntity
    {
       
        public string MeetingEventId { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string ProfileId { get; set; }

        // EF
        public MeetingEventEntity MeetingEvent { get; set; }
    }
}
