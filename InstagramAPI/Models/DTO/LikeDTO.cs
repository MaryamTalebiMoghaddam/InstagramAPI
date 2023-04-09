using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Models.DTO
{
    public class PostGetLikesDTO : PeriodDTO
    {
        [Required]
        public int PageId { get; set; }

        [Required]
        public int PostId { get; set; }

   
    }

}
