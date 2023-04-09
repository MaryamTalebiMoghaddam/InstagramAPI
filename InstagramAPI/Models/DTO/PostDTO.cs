using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Models.DTO
{
    public class PostDTO
    {
        [Required]
        public int PageId { get; set; }


        [StringLength(1998)]
        public string Caption { get; set; }


        [Required]
        public bool MainFileIsImage { get; set; }

        [Required]
        [StringLength(249)]
        public string CoverImageUrl { get; set; }


        public PostFileDTO[] PostFiles { get; set; }
    }

    public class PostCaptionDTO
    {
        [StringLength(1998)]
        public string Caption { get; set; }


        [Required]
        public int PostId { get; set; }


        [Required]
        public int PageId { get; set; }
    }

    public class PostTurnOffCommentingDTO
    {

        [Required]
        public int PostId { get; set; }

        [Required]
        public int PageId { get; set; }
    }


    public class GetPostByPostIdDTO
    {
        [Required]
        public int MyPageId { get; set; }

        [Required]
        public int TargetPageId { get; set; }

        [Required]
        public int PostId { get; set; }
    }

    public class GetPostsByPageDTO : PeriodDTO
    {
        public int? MyPageId { get; set; }
        [Required]
        public int PageId { get; set; }

        [Required]
        public DateTime RequestTime { get; set; }
    }

    public class PostLikeDTO
    {
        public int PageId { get; set; }

        public int PostId { get; set; }

        public int LikerPageId { get; set; }
    }
}
