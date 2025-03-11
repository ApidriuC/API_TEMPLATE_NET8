using Microsoft.AspNetCore.Localization;
using System.Globalization;
using TaskProject.Manager.Application.Configuration;
using TaskProject.Manager.Base.Exceptions;
using TaskProject.Manager.Base.Interfaces;
using TaskProject.Manager.Domain.Interfaces;
using TaskProject.ManagerApi.Configuration.Base;
using TaskProject.ManagerApi.Configuration.Services;

//Inicialización de la aplicación.
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Agregar servicios transversales.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Agregar servicios Dominio.
builder.Services.AddDependencies();

var connectionStrings = config.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>() ??
    throw new InvalidOperationException($"Configuration key '{nameof(ConnectionStrings)}' not found.");

// Agregar servicios API.
builder.Services.AddScoped<IErrorHandler, ErrorHandler>();
builder.Services.AddSingleton<IConnectionStrings>(connectionStrings);
builder.Services.AddTransient<IDataBaseManager, DataBaseManager>();

var app = builder.Build();

// Interfaz Swagger.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(
    new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(new CultureInfo("es-CO"))
    }
);

// Middleware's API.
app.UseCors();
app.UseRouting();
app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorMiddleware>();
app.Run();
