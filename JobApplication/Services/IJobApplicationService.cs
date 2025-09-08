using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Services
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<Jobapplication>> GetAllApplications();
        Task<ActionResult<Jobapplication>> GetApplication(int id);
        Task<ActionResult<Jobapplication>> GetApplicationDetails(int id);
        Task AddApplication(Jobapplication app);
        Task UpdateApplication(int id, Jobapplication app);
        Task DeleteApplication(int id);
    }
}
