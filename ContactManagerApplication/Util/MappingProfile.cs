using AutoMapper;
using ContactManager.DataAccess.Models;
using ContactManagerApplication.Dtos;

namespace Casino.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonCreate, Person>()
                .ForMember(dest => dest.Id, opt => Guid.NewGuid())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Married, opt => opt.MapFrom(src => src.Married))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary));
        }
    }
}
