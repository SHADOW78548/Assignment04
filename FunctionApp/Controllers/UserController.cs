using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
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

        [FunctionName("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/add")] HttpRequest req)
        {
            var user = await req.ReadFromJsonAsync<User>();
            var createdUser = _userService.CreateUser(user!);
            _logger.LogInformation("User created successfully.");
            return new OkObjectResult(createdUser);
        }

        [FunctionName("GetAllUsers")]
        public IActionResult GetAllUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req)
        {
            var users = _userService.GetAllUsers();
            _logger.LogInformation("All users retrieved successfully.");
            return new OkObjectResult(users);
        }

        [FunctionName("GetUserById")]
        public IActionResult GetUserById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequest req, int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return new NotFoundResult();

            _logger.LogInformation("User retrieved successfully.");
            return new OkObjectResult(user);
        }

        [FunctionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{id}")] HttpRequest req, int id)
        {
            var user = await req.ReadFromJsonAsync<User>();
            var updatedUser = _userService.UpdateUser(id, user!);
            if (updatedUser == null) return new NotFoundResult();

            _logger.LogInformation("User updated successfully.");
            return new OkObjectResult(updatedUser);
        }

        [FunctionName("DeleteUser")]
        public IActionResult DeleteUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users/{id}")] HttpRequest req, int id)
        {
            var deletedUser = _userService.DeleteUser(id);
            if (deletedUser == null) return new NotFoundResult();

            _logger.LogInformation("User deleted successfully.");
            return new OkObjectResult(deletedUser);
        }
    }
}
