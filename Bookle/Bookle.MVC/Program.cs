using Bookle.DAL.Contexts;
using Bookle.DAL;

using Microsoft.EntityFrameworkCore;
using Bookle.BL;
using Bookle.Core.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

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
	opt.Password.RequiredLength = 3;
	opt.Password.RequireDigit = false;
	opt.Password.RequireLowercase = false;
	opt.Password.RequireUppercase = false;
	opt.Lockout.MaxFailedAccessAttempts = 1;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(int.MaxValue);
}).AddDefaultTokenProviders().AddEntityFrameworkStores<BookleDbContext>();

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
			name: "areas",
			pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
		  );


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
