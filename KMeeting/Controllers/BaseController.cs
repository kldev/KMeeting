using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KMeeting.DAL.Meeting;
using KMeeting.Repository;

namespace KMeeting.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<IdentityUser> UserManager;
        protected readonly IProfileRepository ProfileRepository;
        protected readonly IMeetingRepository MeetingRepository;

      

        protected BaseController(UserManager<IdentityUser> userManager, IProfileRepository profileRepository, IMeetingRepository meetingRepository = null)
        {
            UserManager = userManager;
            ProfileRepository = profileRepository;
            MeetingRepository = meetingRepository;
        }

        protected async Task<ProfileEntity> GetProfile()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            var profile = await ProfileRepository.GetAsync(user.Id);
            if (profile != null)
            {
                profile.Email = user.Email;
            }
            return profile;

        }
    }
}
