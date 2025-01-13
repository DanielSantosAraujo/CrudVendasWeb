using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using VendasWebMvc.Data;
using VendasWebMvc.Services;
using static System.Formats.Asn1.AsnWriter;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VendasWebMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StringConexao") ?? throw new InvalidOperationException("Connection string 'VendasWebMvcContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<VendedorService>();
builder.Services.AddScoped<DepartamentoServices>();
builder.Services.AddScoped<VendasServices>();

//builder.Services.AddDbContext<VendasWebMvcContext>(options =>
//{
//    var stringConexao = builder.Configuration.GetConnectionString("StringConexao");
//    options.UseSqlServer(stringConexao);
//});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>(); 
    seedingService.Seed();
}

var enUS = new CultureInfo("en-US"); 
var localizationOptions = new RequestLocalizationOptions 
{ DefaultRequestCulture = new RequestCulture(enUS), 
    SupportedCultures = new List<CultureInfo> { enUS }, 
    SupportedUICultures = new List<CultureInfo> { enUS } 
};

app.UseRequestLocalization(localizationOptions);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
