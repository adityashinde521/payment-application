using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using WebAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        var resolver = options.SerializerSettings.ContractResolver;
        if (resolver != null)
            (resolver as DefaultContractResolver).NamingStrategy = null;
    });

// Add CORS policy and configure it
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        //policy.WithOrigins("http://localhost:8080")  // Allow requests from WebApp
             .AllowAnyHeader()
             .AllowAnyMethod();
    });
   
});

// Configure DbContext with SQL Server
builder.Services.AddDbContext<PaymentDetailContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));


// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "WebAPI",
        Version = "v1"
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDetailContext>();
    dbContext.Database.Migrate(); // Automatically applies pending migrations
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI V1");
        c.RoutePrefix = "swagger"; // Use "swagger" if root access isn't required
    });
}

// Use the defined CORS policy
app.UseCors("AllowAll");  //for Prod http://localhost:8080

app.MapControllers();  // Map controllers (equivalent to `UseMvc()` in previous versions)

app.Run();
