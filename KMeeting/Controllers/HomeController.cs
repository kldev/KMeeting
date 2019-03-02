using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KMeeting.DAL.Meeting;
using Microsoft.AspNetCore.Mvc;
using KMeeting.Models;
using KMeeting.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KMeeting.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(UserManager<IdentityUser> userManager, IProfileRepository profileRepository, IMeetingRepository meetingRepository ) :
            base(userManager, profileRepository, meetingRepository)
        {
        }

        public async Task<IActionResult> Index(string message)
        {
            var profile = await GetProfile();

            if (profile == null)
            {
                return Redirect("Profile");
            }

            var meetings = await MeetingRepository.ListAsync();

            if (string.IsNullOrEmpty(message)) message = "";

            ViewData["Message"] = message;

            return View(meetings);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Join(String id)
        {
            var profile = await GetProfile();
            var meeting = await MeetingRepository.GetAsync(id);

            if (meeting.Attendees.FirstOrDefault(x => x.ProfileId.Equals(profile.Id)) != null)
            {                
                return RedirectToAction("Index", new { message = "Already jointed" });
            }
            else
            {
                meeting.Attendees.Add(new MeetingAttendeeEntity
                {
                   MeetingEventId = id, Email = profile.Email, Fullname = profile.Fullname, ProfileId = profile.Id
                });

                await MeetingRepository.UpdateAsync(meeting);
            }

            return RedirectToAction("Index", new { message = "Join game successfully" });
        }

        public async Task<IActionResult> Leave(String meetingId)
        {
            var profile = await GetProfile();
            var meeting = await MeetingRepository.GetAsync(meetingId);

            var attendee = meeting.Attendees.FirstOrDefault(x => x.ProfileId.Equals(profile.Id));
            if (attendee == null)
            {
                return RedirectToAction("Index", new { message = "Not attending the meeting" });
            }
            else
            {
                meeting.Attendees.Remove(attendee);
                await MeetingRepository.UpdateAsync(meeting);
            }

            return RedirectToAction("Index", new { message = "Left the meeting successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json(AjaxOperation.Failed("Wrong Id"));
            
            try
            {
                await MeetingRepository.DeleteAsync(id);
            }
            catch
            {
                return Json(AjaxOperation.Failed("Internal server error"));
            }

            return Json(AjaxOperation.Successful("Record deleted"));
        }
    }
}
