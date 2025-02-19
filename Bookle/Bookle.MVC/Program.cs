using Bookle.DAL.Contexts;
using Bookle.DAL;
using Bookle.BL.Extentions;

using Microsoft.EntityFrameworkCore;
using Bookle.BL;
using Bookle.Core.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// ? Session konfiqurasiyasý ?lav? edildi
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Sessiya 30 d?qiq? aktiv qalacaq
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<BookleDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
});
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
	opt.User.RequireUniqueEmail = true;
	opt.SignIn.RequireConfirmedEmail = false;
	opt.Password.RequiredLength = 3;
	opt.Password.RequireDigit = false;
	opt.Password.RequireLowercase = false;
	opt.Password.RequireUppercase = false;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Lockout.MaxFailedAccessAttempts = 2;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(int.MaxValue);
}).AddDefaultTokenProviders().AddEntityFrameworkStores<BookleDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ? Session-i istifad? etm?zd?n ?vv?l aktiv et!
app.UseSession();

app.UseAuthorization();
app.UseUserSeed();

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
