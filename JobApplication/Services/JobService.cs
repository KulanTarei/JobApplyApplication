using JobApplication.Models;
using JobApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _repo;

        public JobService(IJobRepository repo)
        {
            _repo = repo;
        }
        public async Task AddJob(Job job)
        {
            await _repo.CreateJob(job);
        }

        public async Task DeleteJob(int id)
        {
            await _repo.DeleteJob(id);
        }

        public async Task UpdateJob(int id, Job job)
        {
            await _repo.EditJob(id, job);
        }

        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await _repo.GetAllJobs();
        }

        public async Task<ActionResult<Job>> GetJobById(int id)
        {
            return await _repo.GetJob(id);
        }

        public Task<ActionResult<Job>> GetJobDetails(int id)
        {
            return _repo.GetJobDetails(id);
        }
    }
}
