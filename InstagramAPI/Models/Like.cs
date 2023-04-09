using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstagramAPI.Models
{
    [Table("T_Like")]
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int PageId { get; set; }
        public int PostId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
        //public User UserLikes { get; set; }
    }
}
