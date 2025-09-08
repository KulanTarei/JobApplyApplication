using Dapper;
using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace JobApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        private readonly string _conn;

        private JobapplicationdbContext context;

        private IDbConnection GetConnection()
        {
            IDbConnection conn = new MySqlConnection(_conn);
            return conn;
        }

        public UserRepository(IConfiguration config)
        {
            _config = config;
            _conn = config.GetConnectionString("JobApplicationDB");
        }

        public async Task CreateUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }

        }

        public async Task EditUser(int id, User user)
        {
            var us = await context.Users.FindAsync(id);
            if (us != null)
            {
                context.Entry(us).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM USERS";
            var user = await connection.QueryAsync<User>(sql);
            return user;
        }

        public async Task<ActionResult<User>> GetUser(int id)
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM USERS WHERE ID = @id";
            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
            return user;
        }

        public async Task<ActionResult<User>> GetUserDetails(int id)
        {
            var connection = GetConnection();

            string sql = @"
                SELECT u.*, j.*
                FROM Users u
                INNER JOIN Jobapplications j ON j.Id = u.Id
                WHERE u.Id = @Id";

            var res = (await connection.QueryAsync<User, Jobapplication, User>(
        sql,
        (user, jobapplication) =>
        {
            user.Jobapplication = jobapplication;
            return user;
        },
        new { Id = id },
        splitOn: "Id"
        )).FirstOrDefault();

            return res;
        }
    }
}
