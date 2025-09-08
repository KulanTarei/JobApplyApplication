using Dapper;
using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace JobApplication.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IConfiguration _config;

        private readonly string _conn;

        private JobapplicationdbContext context;

        private IDbConnection GetConnection()
        {
            IDbConnection conn = new MySqlConnection(_conn);
            return conn;
        }

        public JobRepository(IConfiguration config)
        {
            _config = config;
            _conn = config.GetConnectionString("JobApplicationDB");
        }

        public async Task CreateJob(Job job)
        {
            await context.Jobs.AddAsync(job);
            await context.SaveChangesAsync();
        }

        public async Task DeleteJob(int id)
        {
            var job = await context.Jobs.FindAsync(id);
            if (job != null)
            {
                context.Jobs.Remove(job);
                await context.SaveChangesAsync();
            }

        }

        public async Task EditJob(int id, Job job)
        {
            var jo = await context.Jobs.FindAsync(id);
            if (jo != null)
            {
                context.Entry(jo).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM JOBS";
            var job = await connection.QueryAsync<Job>(sql);
            return job;
        }

        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM JOBS WHERE ID = @id";
            var job = await connection.QueryFirstOrDefaultAsync<Job>(sql, new { id });
            return job;
        }

        public async Task<ActionResult<Job>> GetJobDetails(int id)
        {
            var connection = GetConnection();

            string sql = @"
                SELECT jo.*, j.*
                FROM Jobs jo
                INNER JOIN Jobapplications j ON j.Id = jo.Id
                WHERE jo.Id = @Id";

            var res = (await connection.QueryAsync<Job, Jobapplication, Job>(
        sql,
        (job, jobapplication) =>
        {
            job.Jobapplication = jobapplication;
            return job;
        },
        new { Id = id },
        splitOn: "Id"
        )).FirstOrDefault();

            return res;
        }
    }
}
