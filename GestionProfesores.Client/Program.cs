using GestionProfesores.Client;
using GestionProfesores.Client.Servicios;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Registro CORRECTO de servicios

builder.Services.AddScoped<IHttpServicio, HttpServicio>();
builder.Services.AddScoped<IAlumnoServicio, AlumnoServicio>();
builder.Services.AddScoped<IMateriaServicio, MateriaServicio>();
builder.Services.AddScoped<INotaServicio, NotaServicio>();
builder.Services.AddScoped<IAsistenciaServicio, AsistenciaServicio>();


await builder.Build().RunAsync();