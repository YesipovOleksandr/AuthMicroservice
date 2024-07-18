using AuthMicroservice.API.ViewModels;
using AutoMapper;

namespace AuthMicroservice.API.MappingProfile
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<LoginViewModel, Domain.Models.User>().ReverseMap();
            CreateMap<RegisterViewModel, Domain.Models.User>().ReverseMap();
            CreateMap<AuthViewModel, Domain.Models.User>().ReverseMap();
        }
    }
}
