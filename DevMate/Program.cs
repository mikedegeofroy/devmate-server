using System.Text;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Extensions;
using DevMate.Infrastructure.DataAccess.Extensions;
using DevMate.Infrastructure.DataAccess.PostgreSql.Extensions;
using DevMate.Infrastructure.FileSystem.Extensions;
using DevMate.Infrastructure.Integration.Mailgun.Extensions;
using DevMate.Infrastructure.Integration.Telegram.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructureIntegrationMailgun();
builder.Services.AddInfrastructureIntegrationTelegram();
builder.Services.AddInfrastructureLocalFileSystem();
builder.Services.AddInfrastructureDataAccess();
builder.Services.AddInfrastructureDataAccessPostgreSql(configuration =>
{
    configuration.Host = "localhost";
    configuration.Port = 5432;
    configuration.Username = "postgres";
    configuration.Password = "postgres";
    configuration.Database = "postgres";
    configuration.SslMode = "Prefer";
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard basic Authorization bearer token etc etc.",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:JwtToken").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});


WebApplication app = builder.Build();

app.UsePathBase("/api");

ITelegramBot botService = app.Services.GetRequiredService<ITelegramBot>();
botService.Start();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(o =>
{
    o.AllowAnyOrigin();
    o.AllowAnyHeader();
    o.AllowAnyMethod();
});

app.MapControllers();

app.Run();