using InstagramAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Data
{

    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FollowManager> FollowManagers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles").HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins").HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();





            builder.Entity<FollowManager>().HasKey(t => new
            {
                t.UserId,
                t.TargetId
            });


            builder.Entity<User>().HasMany(t => t.Posts).WithOne(t => t.User).HasForeignKey(t => t.PageId);


            //Make PhoneNumber as a unique field
            builder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
            
        }
    }
}
