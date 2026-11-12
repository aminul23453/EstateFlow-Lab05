
using EstateFlow.Application;
using EstateFlow.Application.Contracts.Properties;
using EstateFlow.Application.Services;
using EstateFlow.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
});

// Configure PostgreSQL database
builder.Services.AddDbContext<EstateFlowDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("EstateFlowDbConnection")));

// Register IApplicationDbContext
builder.Services.AddScoped<IApplicationDbContext>(
    provider => provider.GetRequiredService<EstateFlowDbContext>());

// Register validators from the Application project
builder.Services.AddValidatorsFromAssemblyContaining<CreatePropertyRequestValidator>();

// Register services
builder.Services.AddScoped<IMeterReadingService, MeterReadingService>();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "EstateFlow API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
