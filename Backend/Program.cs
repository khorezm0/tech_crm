using System.Data;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Business.Services;
using Authentication.Extensions;
using DAL.Users;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Add Service Injection
builder.Services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(connectionString));
builder.Services.AddSingleton<UsersDal>(); //TODO: interfaces
builder.Services.AddSingleton<UsersService>();
builder.Services.AddControllers();
builder.Services.AddCustomSwagger()
    .AddIdentityServices(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserContextMiddleware>();
app.MapControllers();
app.Run();
