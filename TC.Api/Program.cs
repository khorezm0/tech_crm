using NLog.Web;
using TC.AspNetCore.DependencyInjection;
using TC.AspNetCore.Configurations;
using TC.Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterByDiAttribute("TC.*");
builder.Services.AddControllers();
builder.Services.AddCustomSwagger()
    .AddIdentityServices(builder.Configuration)
    .AddJwt()
    .ConfigureTcOptions(builder.Configuration);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

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
