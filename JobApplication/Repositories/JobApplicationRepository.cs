using Dapper;
using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace JobApplication.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly IConfiguration _config;

        private readonly string _conn;

        private JobapplicationdbContext context;

        private IDbConnection GetConnection()
        {
            IDbConnection conn = new MySqlConnection(_conn);
            return conn;
        }

        public JobApplicationRepository(IConfiguration config)
        {
            _config = config;
            _conn = config.GetConnectionString("JobApplicationDB");
        }
        public async Task CreateApplication(Jobapplication app)
        {
            await context.Jobapplications.AddAsync(app);
            await context.SaveChangesAsync();
        }

        public async Task DeleteApplication(int id)
        {
            var app = await context.Jobapplications.FindAsync(id);
            if (app != null)
            {
                context.Jobapplications.Remove(app);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditApplication(int id, Jobapplication app)
        {
            var jo = await context.Jobapplications.FindAsync(id);
            if (jo != null)
            {
                context.Entry(jo).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Jobapplication>> GetAllApplications()
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM JOBAPPLICATIONS";
            var job = await connection.QueryAsync<Jobapplication>(sql);
            return job;
        }

        public async Task<ActionResult<Jobapplication>> GetApplication(int id)
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM JOBAPPLICATIONS WHERE ID = @id";
            var job = await connection.QueryFirstOrDefaultAsync<Jobapplication>(sql, new { id });
            return job;
        }

        public async Task<ActionResult<Jobapplication>> GetApplicationDetails(int id)
        {
            var connection = GetConnection();

            string sql = @"
                SELECT j.*, u.*, j.*
                FROM Jobapplications j
                INNER JOIN Users u ON u.Id = j.Id
                INNER JOIN Jobs jo ON jo.Id = j.Id
                WHERE j.Id = @Id";

            var res = (await connection.QueryAsync<Jobapplication, User, Job, Jobapplication>(
        sql,
        (jobapplication, user, job) =>
        {
            jobapplication.User = user;
            jobapplication.Job = job;
            return jobapplication;
        },
        new { Id = id },
        splitOn: "Id, Id"
        )).FirstOrDefault();

            return res;
        }
    }
}
