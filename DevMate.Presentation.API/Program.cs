using System.Text;
using DevMate.Application.Extensions;
using DevMate.Infrastructure.DataAccess.PostgreSql.Extensions;
using DevMate.Infrastructure.FileSystem.Extensions;
using DevMate.Infrastructure.Integration.Telegram.User.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddApplication();
builder.Services.AddInfrastructureIntegrationTelegram();
builder.Services.AddInfrastructureLocalFileSystem();
builder.Services.AddInfrastructureDataAccessPostgreSql(configuration =>
{
    configuration.Host = "localhost";
    configuration.Port = 5432;
    configuration.Username = "postgres";
    configuration.Password = "postgres";
    configuration.Database = "postgres";
    configuration.SslMode = "Prefer";
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("my super duper secret key holy shit goddamn fuck off already common"))
    };
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(corsPolicyBuilder =>
        corsPolicyBuilder
            .WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader());
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();