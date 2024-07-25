using System.ComponentModel.DataAnnotations;

namespace TwnTw_WEB.Models
{
    public class MemberDetail
    {
        [Key, Required]
        public Guid MemberDetailId { get; set; }
        public Guid WorkSpaceId { get; set; }
        public Workspace Workspaces { get; set; }   
        public Guid UserId { get; set; }
        public User Users { get; set; } 
        public string Status { get; set; }
        public string Role { get; set; }
    }
}
