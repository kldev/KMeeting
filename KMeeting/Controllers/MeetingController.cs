using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMeeting.DAL.Meeting;
using KMeeting.Models;
using KMeeting.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KMeeting.Controllers
{
    public class MeetingController : BaseController
    {
      
        public MeetingController(UserManager<IdentityUser> userManager, IProfileRepository profileRepository, IMeetingRepository meetingRepository = null) 
            : base(userManager, profileRepository, meetingRepository)
        {
        }

        public async Task<IActionResult> Index(String meetingId = "")
        {
            var model = new MeetingEventModel
            {
                DayStart = DateTime.Now,
                Name = "",
                TimeStart = "12:00",
            };

            if (!string.IsNullOrEmpty(meetingId))
            {
                var meetingEventEntity = await MeetingRepository.GetAsync(meetingId);

                if (meetingEventEntity != null)
                {
                    model = new MeetingEventModel()
                    {
                        Id = meetingId,
                        Status = meetingEventEntity.Status,
                        Name = meetingEventEntity.Name,
                        TimeStart = meetingEventEntity.TimeStart,
                        DayStart = meetingEventEntity.DayStart,
                        MaxPeople = meetingEventEntity.MaxPeople,
                        City = meetingEventEntity.City,
                        Address =  meetingEventEntity.Address
                    };
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MeetingEventModel model)
        {
            var profile = await GetProfile();

            if (string.IsNullOrEmpty(model.Id))
            {
                var attendee = new MeetingAttendeeEntity()
                {
                  Email = profile.Email, Fullname = profile.Fullname, ProfileId = profile.Id, 
                };

                var newGameEvent = new MeetingEventEntity
                {
                    DayStart = model.DayStart,
                    Name = model.Name,
                    TimeStart = model.TimeStart,
                    Attendees = new List<MeetingAttendeeEntity> { attendee },
                    Status = model.Status,
                    MaxPeople = model.MaxPeople,
                    City = model.City,
                    Address = model.Address
                };

                await MeetingRepository.AddAsync(newGameEvent);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var meetingEventEntity = await MeetingRepository.GetAsync(model.Id);
                meetingEventEntity.Status = model.Status;
                meetingEventEntity.Name = model.Name;
                meetingEventEntity.TimeStart = model.TimeStart;
                meetingEventEntity.DayStart = model.DayStart;
                meetingEventEntity.MaxPeople = model.MaxPeople;
                meetingEventEntity.Address = model.Address;
                meetingEventEntity.City = model.City;

                await MeetingRepository.UpdateAsync(meetingEventEntity);

                return RedirectToAction("Index", "Home");

            }

        }

        public async Task<IActionResult> Details(String id)
        {
            var meeting = await MeetingRepository.GetAsync(id);

            if (meeting == null) return NotFound();

            return View(meeting);
        }
    }
}
