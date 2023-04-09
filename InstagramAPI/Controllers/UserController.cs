using InstagramAPI.Models;
using InstagramAPI.Models.DTO;
using InstagramAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InstagramAPI.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        protected APIResponse _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new();
        }


        /// <summary>
        /// this method is using for login with a token
        /// </summary>
        /// <response code="200">every thing is ok</response>
        ///  <response code="400">token or username & password is invalid</response>
        /// 

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (loginResponse==null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErroreMessage.Add("Username or Password is incorrect ");
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }



        /// <summary>
        /// this method is using for signup 
        /// </summary>
        /// <response code="201">user created</response>
        ///  <response code="400">UserName alredy exist or Errore while registering </response>
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignupDTO model)
        {
            bool ifUserIsUniqe = await _userRepository.CheckUserName(model.UserName);
            if (!ifUserIsUniqe)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErroreMessage.Add("this UserName alredy exist!");
                _response.IsSuccess = false;
                return BadRequest(_response);            
            }
            var user = await _userRepository.SignUp(model);
            if (user==null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErroreMessage.Add("Errore while registering!");
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            _response.Result = user;
            return Ok(_response);

        }




    }
}
