using FunctionApp.Models;
using System.Collections.Generic;

namespace FunctionApp.Services
{
    public interface IUserService
    {
        public User CreateUser(User user);
        public IEnumerable<User> GetAllUsers();
        public User GetUserById(int id);
        public User UpdateUser(int id, User user);
        public User DeleteUser(int id);
    }
    
}