using AutoMapper;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.ModelProfile
{
    public class WorkspaceProfile : Profile
    {
        public WorkspaceProfile() 
        { 
            CreateMap<Workspace, WorkspaceCreateModel>().ReverseMap(); // A -> B, B -> A
            CreateMap<Workspace, WorkspaceUpdateModel>().ReverseMap();
            CreateMap<Workspace, WorkspaceViewModel>().ReverseMap();
        }
    }
}
