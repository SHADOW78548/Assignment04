using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionApp.Services;
using FunctionApp.Models;

namespace FunctionApp.Controllers
{
    public class UserController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [Function("CreateUser")]
        public async Task<HttpResponseData> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/add")] HttpRequestData req)
        {
            var user = await req.ReadFromJsonAsync<User>();
            var createdUser = _userService.CreateUser(user!);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(createdUser);
            _logger.LogInformation("User created successfully.");
            return response;
        }

        [Function("GetAllUsers")]
        public async Task<HttpResponseData> GetAllUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req)
        {
            var users = _userService.GetAllUsers();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(users);
            _logger.LogInformation("All users retrieved successfully.");
            return response;
        }

        [Function("GetUserById")]
        public async Task<HttpResponseData> GetUserById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequestData req, int id)
        {
            var user = _userService.GetUserById(id);
            var response = req.CreateResponse(user == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (user == null)
            {
                await response.WriteAsJsonAsync(new { message = "User not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(user);
            }

            _logger.LogInformation("User retrieved successfully.");
            return response;
        }

        [Function("UpdateUser")]
        public async Task<HttpResponseData> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{id}")] HttpRequestData req, int id)
        {
            var user = await req.ReadFromJsonAsync<User>();
            var updatedUser = _userService.UpdateUser(id, user!);

            var response = req.CreateResponse(updatedUser == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (updatedUser == null)
            {
                await response.WriteAsJsonAsync(new { message = "User not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(updatedUser);
            }

            _logger.LogInformation("User updated successfully.");
            return response;
        }

        [Function("DeleteUser")]
        public async Task<HttpResponseData> DeleteUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users/{id}")] HttpRequestData req, int id)
        {
            var deletedUser = _userService.DeleteUser(id);
            var response = req.CreateResponse(deletedUser == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);

            if (deletedUser == null)
            {
                await response.WriteAsJsonAsync(new { message = "User not found" });
            }
            else
            {
                await response.WriteAsJsonAsync(deletedUser);
            }

            _logger.LogInformation("User deleted successfully.");
            return response;
        }
    }
}
