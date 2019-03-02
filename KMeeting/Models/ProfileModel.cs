using System;
using System.ComponentModel.DataAnnotations;


namespace KMeeting.Models
{
    public class ProfileModel
    {
        public string Id { get; set; } = "";
           
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Full name")]
        public String Fullname { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Mobile phone")]
        public string Mobile { get; set; }

        [Display(Name = "About you")]
        [MaxLength(500)]
        public string About { get; set; }
    }
}
