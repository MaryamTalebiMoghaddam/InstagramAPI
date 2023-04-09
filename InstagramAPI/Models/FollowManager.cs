using InstagramAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstagramAPI.Models
{
    [Table("T_FollowManager")]
   
    public class FollowManager
    {       
        public int UserId { get; set; }                                           
        public int TargetId { get; set; }
        public User User { get; set; }
    }
}
