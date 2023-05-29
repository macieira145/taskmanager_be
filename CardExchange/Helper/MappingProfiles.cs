using AutoMapper;
using CardExchange.Dto;

namespace CardExchange.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Entities.Task, TaskDTO>();
        }
    }
}
