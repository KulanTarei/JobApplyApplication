using JobApplication.Models;
using JobApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task AddUser(User user)
        {
            await _repo.CreateUser(user);
        }

        public async Task DeleteUser(int id)
        {
                await _repo.DeleteUser(id);
        }

        public async Task UpdateUser(int id, User user)
        {
                await _repo.EditUser(id, user);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repo.GetAllUsers();
        }

        public async Task<ActionResult<User>> GetUserById(int id)
        {
            return await _repo.GetUser(id);
        }

        public Task<ActionResult<User>> GetUserDetails(int id)
        {
            return _repo.GetUserDetails(id);
        }
    }
}
