using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public class ProductTrigger
    {
        private readonly ILogger<ProductTrigger> _logger;

        public ProductTrigger(ILogger<ProductTrigger> logger)
        {
            _logger = logger;
        }

        [Function("ProductTrigger")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Read query string
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            string name = query["name"];

            // Read body
            var body = await req.ReadAsStringAsync();
            if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(body))
            {
                try
                {
                    var data = System.Text.Json.JsonSerializer.Deserialize<dynamic>(body);
                    name = data?.name;
                }
                catch
                {
                    // ignore parse errors
                }
            }

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(responseMessage);
            return response;
        }
    }
}
