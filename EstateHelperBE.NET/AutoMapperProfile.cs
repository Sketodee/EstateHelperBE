﻿using AutoMapper;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;

namespace EstateHelperBE.NET
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, CreateUserDto>().ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.UserName)) .ReverseMap();

            CreateMap<ConsultantGroup, CreateConsultantGroupDto>().ReverseMap();
            CreateMap<ConsultantGroup, GetConsultantGroupDto>().ReverseMap();
            CreateMap<ConsultantGroup, EditConsultantGroupDto>().ReverseMap();
        }
    }
}
