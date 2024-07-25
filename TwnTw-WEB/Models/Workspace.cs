using System.ComponentModel.DataAnnotations;

namespace TwnTw_WEB.Models
{
    public class Workspace
    {
        [Key, Required]
        public Guid WSId { get; set; }
        public string  WSName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public IEnumerable<MemberDetail>? MemberDetails {  get; set; }
    }
}
