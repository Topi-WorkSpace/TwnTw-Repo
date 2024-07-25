using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.Data
{
    public class TwnTwDbContext:DbContext
    {
        public TwnTwDbContext(DbContextOptions<TwnTwDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<MemberDetail> MemberDetails { get; set; }
        public DbSet<TaskDetail> TaskDetails { get; set; }
        
    }
}
