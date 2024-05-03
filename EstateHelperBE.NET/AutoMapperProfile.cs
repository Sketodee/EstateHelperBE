using AutoMapper;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;

namespace EstateHelperBE.NET
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, CreateUserDto>().ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.UserName)) .ReverseMap();
        }
    }
}
