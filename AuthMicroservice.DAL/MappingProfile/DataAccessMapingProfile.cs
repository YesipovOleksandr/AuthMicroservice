using AuthMicroservice.DAL.Entities;
using AutoMapper;

namespace AuthMicroservice.DAL.MappingProfile
{
    public class DataAccessMapingProfile : Profile
    {
        public DataAccessMapingProfile()
        {
            CreateMap<User, Domain.Models.User>().ReverseMap();
            CreateMap<UserRoles, Domain.Models.UserRoles>().ReverseMap();
        }
    }
}
