using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KMeeting.Areas.Identity.Data
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("km_users");
            builder.Entity<IdentityUserLogin<string>>().ToTable("km_user_logins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("km_user_claims");
            builder.Entity<IdentityUserRole<string>>().ToTable("km_user_roles");
            builder.Entity<IdentityUserToken<string>>().ToTable("km_user_tokens");

            builder.Entity<IdentityRole>().ToTable("km_roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("km_claims");


        }
    }
}
