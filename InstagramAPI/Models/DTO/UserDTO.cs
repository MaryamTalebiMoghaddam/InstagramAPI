using System.ComponentModel.DataAnnotations;

namespace InstagramAPI.Models.DTO
{
    public class UserDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public Random ConfirmCode { get; set; }
        public DateTime TimeToLeave { get; set; }
        public bool AuthorizedPhoneNumber { get; set; }

    }
    public class SignupDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
       

    }
    public class ForgetPasswordDTO
    {
        [Required]
        public string MobileNumber { get; set; }
    }
    public class LoginRequestDTO
    {
        public string? MobileNumber { get; set; }

        [StringLength(40)]         
        public string? Username { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 4)]
        public string Password { get; set; }


    }
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
