using Dapper;
using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace JobApplication.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration _config;

        private readonly string _conn;

        private JobapplicationdbContext context;

        private IDbConnection GetConnection()
        {
            IDbConnection conn = new MySqlConnection(_conn);
            return conn;
        }

        public CompanyRepository(IConfiguration config)
        {
            _config = config;
            _conn = config.GetConnectionString("JobApplicationDB");
        }

        public async Task CreateCompany(Company company)
        {
            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            var company = await context.Companies.FindAsync(id);
            if (company != null)
            {
                context.Companies.Remove(company);
                await context.SaveChangesAsync();
            }

        }

        public async Task EditCompany(int id, Company company)
        {
            var com = await context.Companies.FindAsync(id);
            if (com != null)
            {
                context.Entry(com).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM COMPANIES";
            var com = await connection.QueryAsync<Company>(sql);
            return com;
        }

        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var connection = GetConnection();
            string sql = @"SELECT * FROM COMPANIES WHERE ID = @id";
            var com = await connection.QueryFirstOrDefaultAsync<Company>(sql, new { id });
            return com;
        }

        public async Task<ActionResult<Company>> GetCompanyDetails(int id)
        {
            var connection = GetConnection();

            string sql = @"
                SELECT c.*, j.*
                FROM Companies c
                INNER JOIN Jobs j ON j.Id = c.Id
                WHERE c.Id = @Id";

            var res = (await connection.QueryAsync<Company, Job, Company>(
        sql,
        (company, job) =>
        {
            company.Job = job;
            return company;
        },
        new { Id = id },
        splitOn: "Id"
        )).FirstOrDefault();

            return res;
        }
    }
}
