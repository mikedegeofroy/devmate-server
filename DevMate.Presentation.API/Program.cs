using ParkingApp.Application.Extensions;
using ParkingApp.Infrastructure.DataAccess.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
// builder.Services.AddInfrastructureDataAccess(configuration =>
// {
//     configuration.Host = "localhost";
//     configuration.Port = 5432;
//     configuration.Username = "postgres";
//     configuration.Password = "postgres";
//     configuration.Database = "postgres";
//     configuration.SslMode = "Prefer";
// });

WebApplication app = builder.Build();

// await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
// {
//     await scope.UseInfrastructureDataAccessAsync(default);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();