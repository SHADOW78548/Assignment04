using FunctionApp.Models;
using System.Collections.Generic;
using  FunctionApp.Data;
using FunctionApp.Services;
using System.Linq;
using System;

namespace FunctionApp.Services
{
    public class UserServiceImpl : IUserService
    {

        private readonly FunctionAppDbContext _context;

        public UserServiceImpl(FunctionAppDbContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }
        public User UpdateUser(int id, User user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            _context.SaveChanges();
            return existingUser;
        }
        public User DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return null;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
    
}