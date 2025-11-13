using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using TravelPort.Api.Models;
using TravelPort.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi(opt => opt.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0);
builder.Services.AddDbContext<PgContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IPersistenceService, PersistenceService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection();

app.MapGet("/user/{passportNumber}", async (string passportNumber, IUserService userService) =>
{
    var user = await userService.GetUserByPassportNumber(passportNumber);
    return user != null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/user", async ([FromBody] User user, IUserService userService) =>
{
    var created = await userService.CreateUser(
        user.PassportNumber,
        user.SelectedAirport,
        user.Name,
        user.Surname,
        user.Email,
        null,
        user.PhoneNumber);
    return created ? Results.Ok() : Results.BadRequest();
});

app.MapPut("/user", async ([FromBody] User user, IUserService userService) =>
{
    var created = await userService.UpdateUser(
        user.PassportNumber,
        user.SelectedAirport,
        user.Name,
        user.Surname,
        user.Email,
        null,
        user.PhoneNumber);
    return created ? Results.Ok() : Results.BadRequest();
});

app.MapDelete("/user/{passportNumber}", async (string passportNumber, IUserService userService) =>
{
    var deleted = await userService.DeleteUser(passportNumber);
    return deleted ? Results.Ok() : Results.NotFound();
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PgContext>();
    db.Database.EnsureCreated();
}

app.Run();
