using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#if DEBUG
    if (!System.Diagnostics.Debugger.IsAttached)
    {
        System.Diagnostics.Debugger.Launch();
    }
#endif

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddJsonFile("host.json", optional: true);
        builder.AddJsonFile("local.settings.json", true, true);

        builder.AddEnvironmentVariables();

        builder.Build();
    })
    .ConfigureServices(services => {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var conf = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var test = conf.GetConnectionString("Test");
            var test2 = conf.GetConnectionString("Test2");
            var test3 = conf.GetConnectionString("Test3");
            var test4 = conf.GetConnectionString("Test4");
            var test5 = conf.GetConnectionString("Test5");
            var a = conf.GetConnectionString("AppBlobStorage");
        }
    })
    .Build();

await host.RunAsync();