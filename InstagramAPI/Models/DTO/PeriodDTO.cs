using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Models.DTO
{
    public class PeriodDTO
    {
        [Required]
        [Range(0,int.MaxValue)]
        public int Skip { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Take { get; set; } = 50;
    }
}
