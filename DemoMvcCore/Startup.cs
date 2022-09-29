using DemoMvcCore.DataModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DemoMvcCore
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json");
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. 
        // Use this method to add services to the container. 
        // For more information on how to configure your application, 
        // visit http://go.microsoft.com/fwlink/?LinkID=398940 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")
    ));
            services.ConfigureApplicationCookie
           (options =>
           {
               options.LoginPath = "/Account/Login";
               options.LogoutPath = "/Account/Login";
               options.AccessDeniedPath = "/Account/Login/";
           });
            services.AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme).AddGoogle(option => {
                option.ClientId = "603632684917-574f5j0k2ftcnsat3130v9uve3hhm8qv.apps.googleusercontent.com";
                option.ClientSecret = "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";
                });
           // services.AddAuthorization();
        }

        // This method gets called by the runtime.  
        // Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app)
        {
            //app.UseIISPlatformHandler();

            app.UseDeveloperExceptionPage();
            //app.UseRuntimeInfoPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
           // app.Run();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //to be added
                endpoints.MapRazorPages();
            });

            app.Run(async (context) => {
                var msg = Configuration["message"];
                await context.Response.WriteAsync(msg);

            });
        }
        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            //Home/Index 
            routeBuilder.MapRoute("Default", "{controller = Home}/{action = Index}/{id?}");
        }

        // Entry point for the application. 
        public static void Main(string[] args) 
        {
            WebApplication.CreateBuilder(args); 
        }
    }
}