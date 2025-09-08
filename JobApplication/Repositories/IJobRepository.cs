using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Repositories
{
    public interface IJobRepository
    {
        Task<ActionResult<Job>> GetJobDetails(int id);
        Task<IEnumerable<Job>> GetAllJobs();
        Task<ActionResult<Job>> GetJob(int id);
        Task CreateJob(Job user);
        Task EditJob(int id, Job user);
        Task DeleteJob(int id);
    }
}
