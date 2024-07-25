using System.ComponentModel.DataAnnotations;

namespace TwnTw_WEB.Models
{
    public class User
    {
        [Key, Required]
        public Guid UserId { get; set; }
        public string  Email { get; set; }
        public string  Password { get; set; }
        public string  UserName { get; set; }
        public IEnumerable<MemberDetail> MemberDetails { get; set; }
        public IEnumerable<TaskDetail> TaskDetails { get; set; }
    }
}
