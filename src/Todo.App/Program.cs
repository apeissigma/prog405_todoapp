//for nuget file
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Common.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {

        //registering our services here

        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddTransient<ITaskService, TaskService>(); 

        await builder.Build().RunAsync(); 

    }
}