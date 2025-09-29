using Microsoft.AspNetCore.Routing.Constraints;

namespace DemoMVCSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region ConfigureServices  

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            #endregion
            #region Configure 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller}/{action}/{Id?}", // Id is Optional 
            //    defaults: new {action = "Index" ,controller = "Movies"} ,
            //    constraints : new { Id = @"\d{2}" }  // Id must be a number
            //    );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{Id:regex(^\\d{{2}}$)?}" 
                );
            app.Run(); 
            #endregion

        }
    }
}
