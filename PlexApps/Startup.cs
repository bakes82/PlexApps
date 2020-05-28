using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plex.Api;
using Plex.Api.Api;
using PlexApps.Areas.Identity;
using PlexApps.Data;
using Radzen;

namespace PlexApps
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Create Client Options
            var apiOptions = new ClientOptions
            {
                Platform = "Web",
                DeviceName = "PlexApps(Web)",
                ClientId = "PlexASpps",
                Version = "V1",
                Product = "PlexApps"
            };
            services.AddLogging();
            services.AddSingleton<IApiService, ApiService>();
            services.AddTransient<IPlexClient, PlexClient>();
            services.AddTransient<IPlexRequestsHttpClient, PlexRequestsHttpClient>();
            services.AddSingleton(apiOptions);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(o => o.DetailedErrors = true);
            services
                .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>
                >();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddDevExpressBlazor();
            services.AddScoped<NotificationService>();
            services.AddScoped<DialogService>();
            services.AddProtectedBrowserStorage();
            services.AddSingleton<TraktLoginState>();
            services.AddSingleton<PlexLoginState>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}