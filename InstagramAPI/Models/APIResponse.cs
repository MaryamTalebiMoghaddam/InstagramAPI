using System.Net;

namespace InstagramAPI.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErroreMessage = new List<string>();
        }
        public List<string> ErroreMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
    }
}
