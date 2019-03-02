using KMeeting.DAL.Meeting;
using Microsoft.EntityFrameworkCore;

namespace KMeeting.DAL
{
    public class MeetingContext : DbContext
    {

        public MeetingContext(DbContextOptions<MeetingContext> options)
            : base(options)
        {
        }

        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<MeetingAttendeeEntity> MeetingAttendees { get; set; }
        public DbSet<MeetingEventEntity> Meetings { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileEntity>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode().ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MeetingAttendeeEntity>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.MeetingEvent)
                    .WithMany(s => s.Attendees)
                    .HasForeignKey(e => e.MeetingEventId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<MeetingEventEntity>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode().ValueGeneratedOnAdd();
                entity.HasMany(e => e.Attendees)
                    .WithOne(s => s.MeetingEvent);
            });

            modelBuilder.Entity<ProfileEntity>().ToTable("km_profiles");
            modelBuilder.Entity<MeetingAttendeeEntity>().ToTable("km_attendees");
            modelBuilder.Entity<MeetingEventEntity>().ToTable("km_events");
        }

        
    }
}
