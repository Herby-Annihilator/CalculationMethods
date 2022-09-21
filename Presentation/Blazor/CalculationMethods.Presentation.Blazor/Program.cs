using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Services.Repositories.Double;
using CalculationMethods.Presentation.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<IMatrixRepository<ISquareMatrix<double>, double>, FakeDoubleSquareMatrixRepository>();
builder.Services.AddScoped<IVectorRepository<double>, FakeDoubleVectorRepository>();

await builder.Build().RunAsync();
