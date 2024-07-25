using TwnTw_WEB.Models;

namespace TwnTw_WEB.DTO_Models
{
    public class MemberDetailCreateModel
    {
        public Guid WorkSpaceId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }
}
