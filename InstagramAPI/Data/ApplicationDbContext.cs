using InstagramAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)    
        {

        }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FollowManager> FollowManagers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FollowManager>().HasKey(t=>new { t.UserId,
            t.TargetId});


            builder.Entity<User>().HasMany(t => t.Posts).WithOne(t => t.User).HasForeignKey(t=>t.PageId);


            //Make PhoneNumber as a unique field
            builder.Entity<User>().HasIndex(u=>u.PhoneNumber).IsUnique();
            base.OnModelCreating(builder);
        }
    }
}
