using AutoMapper;
using InstagramAPI.Models;
using InstagramAPI.Models.DTO;

namespace InstagramAPI
{
    public class MapingConfig : Profile
    {
        public MapingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
