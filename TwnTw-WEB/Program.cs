using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Data;
using TwnTw_WEB.ModelProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TwnTwDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("twntw")));
builder.Services.AddAutoMapper(typeof(MemberDetailProfile));
builder.Services.AddAutoMapper(typeof(TaskDetailProfile));
builder.Services.AddAutoMapper(typeof(WorkspaceProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
