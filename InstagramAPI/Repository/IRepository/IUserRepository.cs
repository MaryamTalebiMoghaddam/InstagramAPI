using InstagramAPI.Models.DTO;

namespace InstagramAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> CheckUserName(string username);
        Task<string> Login(LoginRequestDTO loginDTO);
        Task<int> SignUp(SignupDTO signupDTO);
        Task<bool> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO);
        Task<bool> LogOut();
        Task<bool> ConfirmMobileNumber(int confirmCode,int id);
        
    }
}
