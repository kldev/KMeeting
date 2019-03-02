using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMeeting.DAL.Meeting;

namespace KMeeting.Repository
{
    public interface IMeetingRepository
    {
        Task<List<MeetingEventEntity>> ListAsync();
        Task<MeetingEventEntity> AddAsync(MeetingEventEntity meeting);
        Task<MeetingEventEntity> UpdateAsync(MeetingEventEntity meeting);
        Task<MeetingEventEntity> GetAsync(String meetingId);
        Task<bool> DeleteAsync(string meetingId);
    }
}
