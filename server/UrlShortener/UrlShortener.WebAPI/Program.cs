using UrlShortener.BussinessLogic;
using UrlShortener.DataAccess;
using UrlShortener.DataAccess.DbPopulator;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddBussinessLogicServices()
    .AddDataAccessServices(builder.Configuration);

builder.Services.AddRazorPages();

builder.Services.AddControllers();
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
app.UseAuthorization();
app.MapRazorPages();

app.MapControllers();

app.Run();
