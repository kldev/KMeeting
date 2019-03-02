using System;
using System.Threading.Tasks;
using KMeeting.DAL.Meeting;

namespace KMeeting.Repository
{
    public interface IProfileRepository
    {
        Task<ProfileEntity> GetAsync(String identityId);
        Task<ProfileEntity> SaveAsync(ProfileEntity profile);
    }
}
