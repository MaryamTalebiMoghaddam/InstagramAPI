using InstagramAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstagramAPI.Models
{
    [Table("T_Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public int PageId { get; set; }
        public string? Caption { get; set; }      
        public DateTime PostCreateTime { get; set; }
        public DateTime PostUpdateTime { get; set; }
        public bool MainFileIsImage { get; set; }
        public string CoverImageUrl { get; set; }
        public PostFileDTO[] PostFiles { get; set; }
        public bool ArchivedPost { get; set; } = false;
        public bool CommentFlag { get; set; } = true;

        

        public User User { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }



        
        public virtual ICollection<Like> Likes { get; set; }
       
    }
}
