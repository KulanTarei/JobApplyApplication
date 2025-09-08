using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Services
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobs();
        Task<ActionResult<Job>> GetJobById(int id);
        Task<ActionResult<Job>> GetJobDetails(int id);
        Task AddJob(Job job);
        Task UpdateJob(int id, Job job);
        Task DeleteJob(int id);
    }
}
