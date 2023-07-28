using AutoMapper;
using BackendAPI.DTOs;
using BackendAPI.Models;
using System.Globalization;

namespace BackendAPI.Utilities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {

            // Map for PhoneBookDTO
            CreateMap<PhoneBook, PhoneBookDTO>()
            .ForMember(dest => dest.ContactType, opt => opt.MapFrom(src => src.ContactType.Name));

            CreateMap<PhoneBookDTO, PhoneBook>()
            .ForMember(dest => dest.ContactType, opt => opt.Ignore()) 
            .ReverseMap();

            // Map for TypeContactDTO
            CreateMap<TypeContact, TypeContactDTO>().ReverseMap();
            
            CreateMap<string, TypeContactDTO>()
           .ConstructUsing(str => new TypeContactDTO { Name = str });
        }
    }
}
