using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace JobApplication.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<ActionResult<Jobapplication>> GetApplicationDetails(int id);
        Task<IEnumerable<Jobapplication>> GetAllApplications();
        Task<ActionResult<Jobapplication>> GetApplication(int id);
        Task CreateApplication(Jobapplication app);
        Task EditApplication(int id, Jobapplication app);
        Task DeleteApplication(int id);
    }
}
