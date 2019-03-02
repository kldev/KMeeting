using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMeeting.DAL;
using KMeeting.DAL.Meeting;
using Microsoft.EntityFrameworkCore;

namespace KMeeting.Repository
{
    public class MeetingRepository : RepositoryBase, IMeetingRepository
    {
        public MeetingRepository(MeetingContext context)
            :  base(context)
        {
            
        }

        public async Task<List<MeetingEventEntity>> ListAsync()
        {
            return await Context.Meetings.Include(x => x.Attendees)
                .Take(100).OrderByDescending(x => x.DayStart)
                .ToListAsync();
        }

        public async Task<MeetingEventEntity> AddAsync(MeetingEventEntity meeting)
        {
            if (meeting == null) throw new ArgumentException(nameof(meeting));
            if (string.IsNullOrEmpty(meeting.Id)) meeting.Id = Guid.NewGuid().ToString();

            await Context.Meetings.AddAsync(meeting);
            await Context.SaveChangesAsync();

            return await Task.FromResult(meeting);
        }

        public async Task<MeetingEventEntity> UpdateAsync(MeetingEventEntity meeting)
        {
            if (meeting == null) throw new ArgumentException(nameof(meeting));
            if (string.IsNullOrEmpty(meeting.Id)) throw new ArgumentException(nameof(meeting.Id));

            Context.Meetings.Update(meeting);
            await Context.SaveChangesAsync();

            return await Task.FromResult(meeting);
        }

        public async Task<MeetingEventEntity> GetAsync(string meetingId)
        {
            if (string.IsNullOrEmpty(meetingId)) throw new ArgumentException(nameof(meetingId));

            return await Context.Meetings.Include(b => b.Attendees)
                .SingleAsync(x => x.Id.Equals(meetingId));
        }

        public async Task<bool> DeleteAsync(string meetingId)
        {
            if (string.IsNullOrEmpty(meetingId)) throw new ArgumentException(nameof(meetingId));

            var meeting = await Context.Meetings.FirstOrDefaultAsync(x => x.Id.Equals(meetingId));

            if (meeting != null)
            {
                Context.Meetings.Remove(meeting);
                await Context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }
    }
}
