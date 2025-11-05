using NetMatch.Dal.Interfaces;
using NetMatch.Logic.Services;
using NetMatch.DAL.Interfaces;
using NetMatch.DAL.Repositories;
using NetMatch.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("NetmatchDB");

// Register repositories (DAL layer) - these need the connection string
builder.Services.AddScoped<IReisOverzichtRepository, ReisOverzichtRepository>();
builder.Services.AddScoped<IOfferteRepository>(provider => 
    new OfferteRepository(connectionString));

builder.Services.AddScoped<IAccommodationRepository>(provider => 
    new AccommodationRepository(connectionString));

// Register services (Logic layer) - these depend on repository interfaces
builder.Services.AddScoped<ReisOverzichtService>();
builder.Services.AddScoped<OfferteService>();

builder.Services.AddScoped<AccommodationService>();


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

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();