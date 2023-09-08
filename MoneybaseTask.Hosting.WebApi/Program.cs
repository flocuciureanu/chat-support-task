using MoneybaseTask.Hosting.WebApi.Installers;
using MoneybaseTask.Hosting.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container. The following line will call all the classes implementing IInstaller
builder.Services.InstallServicesInAssembly(configuration);
builder.Services.AddSingleton(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddHttpClient();

var app = builder.Build();

// app.UseCors(policyBuilder =>
// {
//     policyBuilder.SetIsOriginAllowed(_ => true)
//         .AllowAnyHeader()
//         .AllowAnyMethod()
//         .AllowCredentials();
// });

app.UseMiddleware<ExceptionHandlingMiddleware>();

// app.UseFileServer();

app.UseSwagger();
app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoneybaseTask.Hosting.WebApi v1"));

app.MapControllers();

app.Run();