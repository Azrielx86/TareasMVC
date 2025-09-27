using AutoMapper;
using TareasMVC.Entities;

namespace TareasMVC.Services;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Entities.Task, TaskDto>();
    }
}