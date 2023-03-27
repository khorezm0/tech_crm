using System.Text.Json.Serialization;
using TC.Api.DependencyInjection;
using TC.AspNetCore.Configurations;
using TC.AspNetCore.Filters;
using TC.Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddRouting()
    .AddControllers(options => options.Filters.Add<InputValidationActionFilter>())
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddCustomSwagger()
    .RegisterDefaultSingletons()
    .AddIdentityServices(builder.Configuration)
    .AddJwt()
    .ConfigureTcOptions(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsBuilder =>
    {
        corsBuilder.SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            // .AllowAnyOrigin()
            .AllowCredentials();
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserContextMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints
        // .AddHubs() // SignalR
        .MapControllers();
});
app.Run();