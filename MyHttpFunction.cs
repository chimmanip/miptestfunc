using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.InformationProtection;

namespace MyFunctionApp
{
    public class MyHttpFunction
    {
        private readonly ILogger<MyHttpFunction> _logger;

        public MyHttpFunction(ILogger<MyHttpFunction> logger)
        {
            _logger = logger;
        }

        [Function("MyHttpFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processing a request.");

            const string clientId = "your-client-id";
            const string appName = "your-app-name";

            MIP.Initialize(MipComponent.File);

            ApplicationInfo appId = new ApplicationInfo()
            {
                ApplicationId = clientId,
                ApplicationName = appName,
                ApplicationVersion = "1.0.0",
            };

            // AuthDelegateImplementation authDelegate = new AuthDelegateImplementation(appId);

            MipConfiguration mipConfiguration = new MipConfiguration(
                    appId,
                    "mip_datafolder",
                    Microsoft.InformationProtection.LogLevel.Trace,
                    false // Set to true if running in offline mode
                );

            MipContext mipContext = MIP.CreateMipContext(mipConfiguration);
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
