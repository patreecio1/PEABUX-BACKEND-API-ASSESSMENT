
using HealthInsuranceERP.API.Configurations;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(AppUrl)).Get<AppUrl>());
builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(MailConfiguration)).Get<MailConfiguration>());

builder.Services.AddMemoryCache();
builder.Services.AppServicesConfiguration(builder.Configuration);
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddDocumentationConfiguration();
builder.Services.AddAuthorization();

string cors = builder.Configuration.GetValue<string>("cors");
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // This allows credentials like cookies to be sent with the request if needed.
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
