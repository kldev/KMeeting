using System;
using System.Threading.Tasks;
using KMeeting.DAL;
using KMeeting.DAL.Meeting;
using Microsoft.EntityFrameworkCore;

namespace KMeeting.Repository
{
    public class ProfileRepository : RepositoryBase, IProfileRepository
    {

        public ProfileRepository(MeetingContext context)
            : base(context)
        {
        }
    
        public async Task<ProfileEntity> GetAsync(string identityId)
        {
            return await Context.Profiles
                .FirstOrDefaultAsync(x => x.IdentityId.Equals(identityId));
        }

        public async Task<ProfileEntity> SaveAsync(ProfileEntity profile)
        {
            if (!string.IsNullOrEmpty(profile.Id))
            {
                Context.Profiles.Update(profile);
                await Context.SaveChangesAsync();
            }
            else
            {
                profile.Id = Guid.NewGuid().ToString();
                var addedRecord = Context.Profiles.Add(profile);
                await Context.SaveChangesAsync();
                profile.Id = addedRecord.Entity.Id;
            }

            return Task.FromResult(profile).Result;
        }
    }
}
