using Microsoft.EntityFrameworkCore;
using Refit;
using Serilog;
using Money.Infrastructure.Data;
using Money.Application.External;
using Money.Api;
using Money.Application;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.File("logs/currency-converter-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Add Entity Framework
builder.Services.AddDbContext<CurrencyDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(CurrencyMappingProfile));

//Inject dependencies
builder.Services.AddApplicationServices();

// Add Refit for Open Exchange Rates API
builder.Services.AddRefitClient<IOpenExchangeRatesApi>()
    .ConfigureHttpClient(c =>
    {
        var baseUrl = builder.Configuration["OpenExchangeRates:BaseUrl"];
        c.BaseAddress = new Uri(baseUrl ?? "https://openexchangerates.org/api");
    });

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Lindul Amaratunga - Money API", 
        Version = "v1",
        Description = "Currency conversion"
    });
    
    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add Problem Details
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add global exception handling
app.UseExceptionHandler("/error");

app.MapGet("/error", () => Results.Problem("An error occurred."))
    .ExcludeFromDescription();

app.Run();
