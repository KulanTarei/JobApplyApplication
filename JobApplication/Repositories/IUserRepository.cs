using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<ActionResult<User>> GetUser(int id);
        Task<ActionResult<User>> GetUserDetails(int id);
        Task CreateUser(User user);
        Task EditUser(int id, User user);
        Task DeleteUser(int id);
    }
}
