using AutoMapper;
using EBSystem.Authentication.API.Dtos;
using EBSystem.Authentication.API.Entities;

namespace EBSystem.Authentication.API.Mapper
{
    public class AuthenticationProfile:Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Login, LoginDto>().ReverseMap();
        }
    }
}
