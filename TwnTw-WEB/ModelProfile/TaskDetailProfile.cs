using AutoMapper;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.ModelProfile
{
    public class TaskDetailProfile : Profile
    {
        public TaskDetailProfile()
        {
            CreateMap<TaskDetail, TaskDetailViewModel>().ReverseMap();
            CreateMap<TaskDetail, TaskDetailCreateModel>().ReverseMap();
            CreateMap<TaskDetail, TaskDetailUpdateModel>().ReverseMap();

        }
    }
}
