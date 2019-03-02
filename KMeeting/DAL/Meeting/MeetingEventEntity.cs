using System;
using System.Collections.Generic;

namespace KMeeting.DAL.Meeting
{
    public class MeetingEventEntity : BaseEntity
    {       
        public int MaxPeople { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public MeetingEventStatus Status { get; set; }
        public DateTime DayStart { get; set; }
        public string TimeStart { get; set; }


        // EF
        public List<MeetingAttendeeEntity> Attendees { get; set; }

        public String Date =>
            $"{DayStart:dd-MM-yyyy}, {TimeStart}";
    }

    public enum MeetingEventStatus
    {
        Open,
        MaxPeople,
        Close
    }
}
