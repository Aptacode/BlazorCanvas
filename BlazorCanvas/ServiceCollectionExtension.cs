using Microsoft.Extensions.DependencyInjection;

namespace Aptacode.BlazorCanvas
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlazorCanvas(this IServiceCollection services)
            => services.AddScoped<BlazorCanvasInterop>();
    }
}