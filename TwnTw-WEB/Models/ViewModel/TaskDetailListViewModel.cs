namespace TwnTw_WEB.Models.ViewModel
{
    public class TaskDetailListViewModel
    {
        public Guid TaskDetailId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
