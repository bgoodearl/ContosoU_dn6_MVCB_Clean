using NLog;
using NLog.Web;
using CU.Infrastructure;
using NLEL = NLog.Extensions.Logging;

InitializeLogging();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(GetUserJsonFilename(), true);

builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Inject HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddInfrastructure(builder.Configuration);

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

app.MapBlazorHub();
//app.MapFallbackToPage("/???"); //TODO

app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

NLog.Logger? logger = null;
DateTime startTimeUtc = DateTime.UtcNow;

try
{
    if (logger == null) logger = NLog.LogManager.GetCurrentClassLogger();

    if (logger != null) logger.Info("ContosoUniversity Main start");

    app.Run();
}
catch (Exception ex)
{
    if (logger == null) logger = NLog.LogManager.GetCurrentClassLogger();
    if (logger != null)
        logger.Error(ex, "ContosoUniversity Main {0}: {1}", ex.GetType().Name, ex.Message);
    throw;
}
finally
{
    TimeSpan elapsed = new TimeSpan(DateTime.UtcNow.Ticks - startTimeUtc.Ticks);
    if (logger == null) logger = NLog.LogManager.GetCurrentClassLogger();
    logger.Info("ContosoUniversity Main end - elapsed: {0}", elapsed);

    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

static void InitializeLogging()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
#if DEBUG
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
#else
        .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
#endif
#if DEBUG
        .AddJsonFile(GetUserJsonFilename(), optional: true, reloadOnChange: true)
#endif
        .Build();
    LogManager.Configuration = new NLEL.NLogLoggingConfiguration(config.GetSection("NLog"));
}

static string GetUserJsonFilename()
{
    return $"appsettings.development_user_{Environment.UserName.ToLower()}.json";
}
