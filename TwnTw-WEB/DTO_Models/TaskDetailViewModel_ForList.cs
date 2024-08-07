using TwnTw_WEB.Models;

namespace TwnTw_WEB.DTO_Models
{
    public class TaskDetailViewModel_ForList //Sử dụng cho một workspace
    {

        //thông tin về workspace
        public Workspace workSpace { get; set; }
        //thông tin về member trong workspace
        public MemberDetail memberDetail { get; set; }
        //thông tin về task
        public List<TaskDetail> taskDetails { get; set; }

    }
}
