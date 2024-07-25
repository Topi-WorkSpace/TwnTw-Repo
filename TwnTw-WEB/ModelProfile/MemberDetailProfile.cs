using AutoMapper;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.ModelProfile
{
    public class MemberDetailProfile :Profile
    {
        public MemberDetailProfile()
        {
            CreateMap<MemberDetail, MemberDetailViewModel>().ReverseMap();
            CreateMap<MemberDetail, MemberDetailCreateModel>().ReverseMap();
            CreateMap<MemberDetail, MemberDetailUpdateModel>().ReverseMap();
        }
    }
}
