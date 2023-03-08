using DAL.Data;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Business.Services;
using Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//Add Service Injection
builder.Services.AddSingleton<UsersRepository>(); //TODO: interfaces
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
app.MapControllers();
app.Run();
