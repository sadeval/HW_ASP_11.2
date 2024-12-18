using HW_ASP_11._2.Data;
using Microsoft.EntityFrameworkCore;

namespace HW_ASP_11._2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<EmailService>(); // Регистрация EmailService

            var app = builder.Build();

            Task.Run(async () =>
            {
                using (var scope = app.Services.CreateScope())
                {
                    var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    while (true)
                    {
                        var upcomingTasks = _context.TodoItems
                            .Where(t => t.DueDate.Date == DateTime.Today.AddDays(1))
                            .ToList();

                        foreach (var task in upcomingTasks)
                        {
                            await emailService.SendReminderEmailAsync("sadeval2011@gmail.com", task.Title, task.DueDate);
                        }

                        await Task.Delay(TimeSpan.FromHours(24));
                    }
                }
            });
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
                pattern: "{controller=Todo}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
