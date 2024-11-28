using UrlShortener.BussinessLogic;
using UrlShortener.DataAccess;
using UrlShortener.DataAccess.DbPopulator;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddBussinessLogicServices()
    .AddDataAccessServices(builder.Configuration);

builder.Services.AddRazorPages();

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

DatabasePopulator.PopulateDbAsync(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // I don't know if it needs for now
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//pattern: "{controller=Home}/{action=Index}");

//app.MapControllerRoute(
//    name: "mvc",
//    pattern: "{controller=Home}/{action=Index}/{id?}",
//    defaults: null,
//    constraints: null,
//    dataTokens: new { Area = "MvcControllers" } // Для MVC-контроллеров
//);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: null,
    constraints: null,
    dataTokens: new { Area = "MvcControllers" } // Для MVC-контроллеров
);

app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Home}/{action=Index}/{id?}"
);


app.Run();