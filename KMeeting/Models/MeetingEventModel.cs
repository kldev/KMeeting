using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KMeeting.DAL.Meeting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KMeeting.Models
{
    public class MeetingEventModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public MeetingEventStatus Status { get; set; } = MeetingEventStatus.Open;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DayStart { get; set; }

        public string TimeStart { get; set; }

        [Display(Name = "Max attendees")]
        public int MaxPeople { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        public List<string> TimeStartOption
        {
            get
            {
                var list = new List<string>();            
                var i = 0;
                while (i < 24)
                {
                    if (i < 10)
                    {
                        list.AddRange(new List<string>
                        {
                            $"0{i}:00",
                            $"0{i}:15",
                            $"0{i}:30",
                            $"0{i}:45",
                        });
                    }
                    else
                    {
                        list.AddRange(new List<string>
                        {
                            $"{i}:00",
                            $"{i}:15",
                            $"{i}:30",
                            $"{i}:45",
                        });
                    }

                    i++;
                }

                return list;
            }
        }


        public List<SelectListItem> TimeStartOptionListItems =>
            (from x in TimeStartOption
                select new SelectListItem
                {
                    Text = x,
                    Value = x
                }).ToList();
    }
}
