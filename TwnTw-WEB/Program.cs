using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Data;
using TwnTw_WEB.ModelProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TwnTwDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cloudConnection")));
builder.Services.AddAutoMapper(typeof(MemberDetailProfile));
builder.Services.AddAutoMapper(typeof(TaskDetailProfile));
builder.Services.AddAutoMapper(typeof(WorkspaceProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAuthentication().AddCookie(options =>
{
    options.LoginPath = "/User/Login";
});
builder.Services.AddSession(options => 
{ 
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
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
//session
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
