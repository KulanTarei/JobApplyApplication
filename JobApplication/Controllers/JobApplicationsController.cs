using JobApplication.Models;
using JobApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _serv;

        public JobApplicationsController(IJobApplicationService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jobapplication>>> GetAllApplications()
        {
            try
            {
                var app = await _serv.GetAllApplications();
                return Ok(app);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Jobapplication>> GetApplicationById(int id)
        {
            try
            {
                var app = await _serv.GetApplication(id);
                if (app == null)
                {
                    return NotFound($"User with ID {id} not found");
                }
                return Ok(app);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetDetails/{id}")]
        public async Task<ActionResult<Company>> GetCompanyDetails(int id)
        {
            try
            {
                var com = await _serv.GetApplicationDetails(id);
                if (com == null)
                {
                    return NotFound($"User with ID {id} not found");
                }

                return Ok(com);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<int> AddApplication(Jobapplication job)
        {
            try
            {
                var appId = _serv.AddApplication(job);
                return CreatedAtAction(nameof(GetApplicationById), new { id = appId }, new { Id = appId });

                if (appId == null)
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateApplication(int id, Jobapplication app)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var success = _serv.UpdateApplication(id, app);
                if (success == null)
                {
                    return NotFound($"User with ID {id} not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplication(int id)
        {
            try
            {
                var deletedApp = _serv.DeleteApplication(id);

                if (deletedApp == null)
                    return NotFound($"User with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
