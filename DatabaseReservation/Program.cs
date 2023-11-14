using DatabaseReservation.Data;
using DatabaseReservation.Models;
using DatabaseReservation.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace DatabaseReservation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("RetailDBConnection") ?? throw new InvalidOperationException("Connection string 'ReservationDbContextConnection' not found.");

            builder.Services.AddDbContext<ReservationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IReservationService, ReservationService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ReservationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped<IUserService, UserService>();
            
            // For routing unauthorised access
            builder.Services.ConfigureApplicationCookie(options =>
            {
                //Location for your Custom Access Denied Page
                options.AccessDeniedPath = "/user/AccessDenied";

                //Location for your Custom Login Page
                options.LoginPath = "/user/Login";
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
            
            app.UseAuthentication();
            app.UseAuthorization();
            // app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}