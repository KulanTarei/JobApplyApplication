using JobApplication.Models;
using JobApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repo;

        public JobApplicationService(IJobApplicationRepository repo)
        {
            _repo = repo;
        }
        public async Task AddApplication(Jobapplication app)
        {
            await _repo.CreateApplication(app);
        }

        public async Task DeleteApplication(int id)
        {
            await _repo.DeleteApplication(id);
        }

        public async Task<IEnumerable<Jobapplication>> GetAllApplications()
        {
            return await _repo.GetAllApplications();
        }

        public async Task<ActionResult<Jobapplication>> GetApplication(int id)
        {
            return await _repo.GetApplication(id);
        }

        public async Task UpdateApplication(int id, Jobapplication app)
        {
            await _repo.EditApplication(id, app);
        }
        public Task<ActionResult<Jobapplication>> GetApplicationDetails(int id)
        {
            return _repo.GetApplicationDetails(id);
        }
    }
}
