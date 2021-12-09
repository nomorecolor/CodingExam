using AutoMapper;
using CodingExam.Domain.Models;
using CodingExam.WebAPI.Dtos;

namespace CodingExam.WebAPI.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Interest, InterestDto>().ReverseMap();
            CreateMap<InterestDetails, InterestDetailsDto>().ReverseMap();
        }
    }
}
