using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Dialogs;
using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Services.Dialogs;
using CalculationMethods.Infrastructure.Services.Factories.Base;
using CalculationMethods.Infrastructure.Services.Repositories.Double;
using CalculationMethods.Presentation.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddScoped<IFactory<double>, DoubleFactory>();
builder.Services.AddScoped<IFactory<string>, StringFactory>();

await builder.Build().RunAsync();
