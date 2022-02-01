using DevIO.App.Data;
using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevIO.App;

public class Startup : IStartup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigurationServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<MyDbContext>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IQuartoRepository, QuartoRepository>();

        services.AddAutoMapper(typeof(Startup));

        services.AddControllersWithViews().AddRazorRuntimeCompilation();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
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

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    }
}

public interface IStartup
{
    IConfiguration Configuration { get; }
    void Configure(WebApplication app, IWebHostEnvironment environment);
    void ConfigurationServices(IServiceCollection services);
}

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder WebAppBuilder)
        where TStartup : IStartup
    {
        if (Activator.CreateInstance(typeof(TStartup), WebAppBuilder.Configuration) is not IStartup startup)
            throw new ArgumentException("Classe Startup.cs inválida!");

        startup.ConfigurationServices(WebAppBuilder.Services);

        var app = WebAppBuilder.Build();

        startup.Configure(app, app.Environment);

        app.Run();

        return WebAppBuilder;
    }
}