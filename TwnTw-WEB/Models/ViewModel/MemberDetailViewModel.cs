namespace TwnTw_WEB.Models.ViewModel
{
    public class MemberDetailViewModel
    {
        public Guid UserId { get; set; }
        public Guid WSId { get; set; }
        public string WSName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
