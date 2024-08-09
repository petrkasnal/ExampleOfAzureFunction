using Aspire.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspireApp4.AppHost
{
    public static class Extension
    {
        public static IResourceBuilder<ExecutableResource> AddAzureFunction<TServiceMetadata>(
            this IDistributedApplicationBuilder builder,
            string name,
            int port,
            int debugPort)
            where TServiceMetadata : IProjectMetadata, new()
        {
            var serviceMetadata = new TServiceMetadata();
            var projectPath = serviceMetadata.ProjectPath;
            var projectDirectory = Path.GetDirectoryName(projectPath)!;

            var args = new[]
            {
            "host",
            "start",
            "--port",
            port.ToString(),
            "--nodeDebugPort",
            debugPort.ToString(),
            "--pause-on-error",
            "–-dotnet-isolated-debug",
            "--debug",
            "--inspect=5858"
        };

            return builder.AddExecutable(name, "func", projectDirectory, args)
                .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Aspire")
                .WithEnvironment("AZURE_FUNCTIONS_ENVIRONMENT", "Aspire")
                .WithEnvironment("DOTNET_ENVIRONMENT", "Aspire")
                .WithOtlpExporter();
        }
    }
}
