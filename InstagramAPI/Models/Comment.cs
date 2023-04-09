using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstagramAPI.Models
{
    [Table("T_Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentText { get; set; }
        public int PageId { get; set; }
        public int PostId { get; set; }
        public int MyPageId { get; set; }
        public int PosterPageId { get; set; }
        public User User { get; set; }

    }
}
