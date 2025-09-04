using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;

namespace ECommerce.Core.MappingProfiles
{
    public class UserMapProfiles: Profile
    {
        public UserMapProfiles()
        {
            CreateMap<ApplicationUser, AuthenticationResponse>().
                ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID)).
                ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).
                ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName)).
                ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).
                ForMember(dest => dest.Token, opt => opt.Ignore()).
                ForMember(dest => dest.Success, opt => opt.Ignore()).ReverseMap();  
            CreateMap<RegisterRequestDTO, ApplicationUser>().
                ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).
                ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password)).
                ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName)).
                ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).ReverseMap();
        }
    }
}
