namespace TwnTw_WEB.DTO_Models
{
    public class TaskDetailViewModel
    {
        public Guid TaskDetailId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
