using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Threading;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FunctionApp7
{
    public class Function1
    {
        private readonly IConfiguration _configuration;
        public Function1(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Microsoft.Azure.Functions.Worker.Function("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var a = _configuration.GetConnectionString("Test");
            var a2 = _configuration.GetConnectionString("Test2");
            var a3 = _configuration.GetConnectionString("Test3");
            var a4 = _configuration.GetConnectionString("Test4");
            var a5 = _configuration.GetConnectionString("Test5");
            log.LogInformation("C# HTTP trigger function processed a request.");

            
            return new OkObjectResult("Hi");
        }
    }
}
