using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Models.DTO
{
    public class GetWithFilterDTO : PeriodDTO
    {
        public int PostId { get; set; }         
        public int MyPageId { get; set; }                        

    }

    public class PostCommentsCountDTO
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public int PageId { get; set; }
    }

    public class PostCommentsCreateDTO
    {
        [Required]
        public int PageId { get; set; }

        [Required]
        public int PostId { get; set; }


        [Required]
        [StringLength(1998)]
        public string Text { get; set; }


        [Required]
        public int MyPageId { get; set; }
    }

    public class PostCommentDeleteDTO
    {
        [Required]
        public int CommentId { get; set; }


        [Required]
        public int PostId { get; set; }


        [Required]
        public int MyPageId { get; set; }
        [Required]
        public int PageId { get; set; }
    }

}
