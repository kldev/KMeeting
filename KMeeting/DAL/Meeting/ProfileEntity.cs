namespace KMeeting.DAL.Meeting
{
    public class ProfileEntity : BaseEntity
    {        
        public string IdentityId { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }
    }
}
