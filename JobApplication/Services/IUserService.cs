using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<ActionResult<User>> GetUserById(int id);
        Task<ActionResult<User>> GetUserDetails(int id);
        Task AddUser(User user);
        Task UpdateUser(int id, User user);
        Task DeleteUser(int id);
    }
}
