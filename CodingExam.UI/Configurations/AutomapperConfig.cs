using AutoMapper;
using CodingExam.Domain.Models;
using CodingExam.UI.Models;

namespace CodingExam.UI.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Interest, InterestViewModel>().ReverseMap();
            CreateMap<InterestDetails, InterestDetailsViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ForMember(u => u.Password, opt => opt.Ignore()).ReverseMap();
        }
    }
}
