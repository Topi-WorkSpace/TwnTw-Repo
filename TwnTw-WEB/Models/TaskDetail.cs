using System.ComponentModel.DataAnnotations;

namespace TwnTw_WEB.Models
{
    public class TaskDetail
    {
        [Key, Required]
        public Guid TaskDetailId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User? Users { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
