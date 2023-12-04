using libermanyankt_42_20.Database;
using libermanyankt_42_20.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using libermanyankt_42_20.Filters.PrepodDegreeFilters;
using libermanyankt_42_20.Interfaces;


namespace libermanyankt_42_20.Controllers
{
    [ApiController]
    [Route("controller")]

    public class DegreesController : Controller
    {
        private readonly ILogger<DegreesController> _logger;
        private readonly IDegreesService _degreesService;
        private PrepodDbContext _context;

        public DegreesController(ILogger<DegreesController> logger, IDegreesService degreeService, PrepodDbContext context)
        {
            _logger = logger;
            _degreesService = degreeService;
            _context = context;
        }

        [HttpPost]
        [Route("GetPrepodsByDegree")]
        public async Task<IActionResult> GetPrepodsByDegreeAsync(PrepodDegreeFilter filter, CancellationToken cancellationToken = default)
        {
            var degrees = await _degreesService.GetPrepodsByDegreeAsync(filter, cancellationToken);

            return Ok(degrees);
        }

        [HttpPost("AddDegree", Name = "AddDegree")]
        public IActionResult CreateDegree([FromBody] Degree degree)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Degree.Add(degree);
            _context.SaveChanges();
            return Ok(degree);
        }

        [HttpPut("EditDegree")]
        public IActionResult UpdateDegree(string name_degree, [FromBody] Degree updatedDegree)
        {
            var existingDegree = _context.Degree.FirstOrDefault(g => g.Name_degree == name_degree);

            if (existingDegree == null)
            {
                return NotFound();
            }

            existingDegree.Name_degree = updatedDegree.Name_degree;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteDegree")]
        public IActionResult DeleteDegree(string name_degree, Degree updatedDegree)
        {
            var existingDegree = _context.Degree.FirstOrDefault(g => g.Name_degree == name_degree);

            if (existingDegree == null)
            {
                return NotFound();
            }
            _context.Degree.Remove(existingDegree);
            _context.SaveChanges();

            return Ok();
        }
    }
}
