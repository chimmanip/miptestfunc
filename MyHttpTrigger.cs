using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.InformationProtection;

namespace MyFunctionApp2
{
    public class MyHttpTrigger
    {
        private readonly ILogger<MyHttpTrigger> _logger;

        public MyHttpTrigger(ILogger<MyHttpTrigger> logger)
        {
            _logger = logger;
        }

        [Function("MyHttpTrigger")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            MIP.Initialize(MipComponent.File);
            ApplicationInfo appInfo = new ApplicationInfo()
            {
                ApplicationId = "",
                ApplicationName = "appName",
                ApplicationVersion = "1.0"
            };
            MipConfiguration mipConfiguration = new MipConfiguration(appInfo, "mip_data", Microsoft.InformationProtection.LogLevel.Trace, false);

            // Create MipContext using Configuration
            MipContext mipContext = MIP.CreateMipContext(mipConfiguration);
            _logger.LogInformation("MipContext created successfully.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
