using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Models.DTO
{
    public class PostFileDTO
    {
        [Key]
        public int PostFileId { get; set; }

        [Required]
        public bool IsImage { get; set; }

        [Required]
        public ushort PartOfPost { get; set; }

        [Required]
        [StringLength(249)]
        public string FileUrl { get; set; }
    }
}
