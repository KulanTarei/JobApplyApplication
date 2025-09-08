using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<ActionResult<Company>> GetCompany(int id);
        Task<ActionResult<Company>> GetCompanyDetails(int id);
        Task CreateCompany(Company company);
        Task EditCompany(int id, Company company);
        Task DeleteCompany(int id);
    }
}
