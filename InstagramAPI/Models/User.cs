using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstagramAPI.Models
{
    public class User : IdentityUser<int>
    {
        //public int Id { get; set; }
        //public ulong MobileNumber { get; set; }
        //public string UserName { get; set; }
        public string Name { get; set; }       
        public int ConfirmCode { get; set; } 
        public DateTime TTL { get; set; }
        public bool AuthorizedPhoneNumber { get; set; } = false;

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<FollowManager>? FollowManagers { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
