using System;
using System.Threading.Tasks;
using KMeeting.DAL.Meeting;
using KMeeting.Models;
using KMeeting.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KMeeting.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(IProfileRepository profileRepository, UserManager<IdentityUser> userManager)
            : base(userManager, profileRepository)
        {
        }

        public async Task<IActionResult> Index()
        {

            var profile = await GetProfile();

            var model = new ProfileModel();

            if (profile != null)
            {
                model.Id = profile.Id;
                
                model.Fullname = profile.Fullname;
                model.About = profile.About;
                model.Mobile = profile.Mobile;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfileModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await UserManager.GetUserAsync(HttpContext.User);

                try
                {
                    var profile = await ProfileRepository.SaveAsync(new ProfileEntity
                    {                      
                        IdentityId = user.Id,
                        Id = model.Id,
                        About = model.About, Mobile = model.Mobile, Email = string.Empty, Fullname = model.Fullname
                    });

                    return RedirectToAction("Index", "Home");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);

        }
    }
}
