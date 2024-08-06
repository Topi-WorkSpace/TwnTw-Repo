using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TwnTw_WEB.Models
{
    public class Workspace
    {
        [Key, Required]
        public Guid WSId { get; set; }
        [DisplayName("Tên dự án")]
        public string  WSName { get; set; }
        [DisplayName("Mô tả dự án")]
        public string Description { get; set; }
        [DisplayName("Trạng thái")]
        public string Status { get; set; }
        public IEnumerable<MemberDetail>? MemberDetails {  get; set; }
    }
}
