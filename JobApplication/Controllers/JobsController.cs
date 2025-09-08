using JobApplication.Models;
using JobApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _serv;

        public JobsController(IJobService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
        {
            try
            {
                var jobs = await _serv.GetAllJobs();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobById(int id)
        {
            try
            {
                var job = await _serv.GetJobById(id);
                if (job == null)
                {
                    return NotFound($"User with ID {id} not found");
                }
                return Ok(job);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetJobDetails/{id}")]
        public async Task<ActionResult<Company>> GetCompanyDetails(int id)
        {
            try
            {
                var com = await _serv.GetJobDetails(id);
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
        public ActionResult<int> AddJob(Job job)
        {
            try
            {
                var jobId = _serv.AddJob(job);
                return CreatedAtAction(nameof(GetJobById), new { id = jobId }, new { Id = jobId });

                if (jobId == null)
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
        public async Task<ActionResult> UpdateJob(int id, Job job)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var success = _serv.UpdateJob(id, job);
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
        public async Task<ActionResult> DeleteJob(int id)
        {
            try
            {
                var deletedJob = _serv.DeleteJob(id);

                if (deletedJob == null)
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
