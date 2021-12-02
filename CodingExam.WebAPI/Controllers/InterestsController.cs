using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestService _interestService;

        public InterestsController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var interests = await _interestService.GetAll();

            return Ok(interests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Interest>> GetById(int id)
        {
            var interest = await _interestService.GetById(id);

            if (interest == null) return NotFound();

            return Ok(interest);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Interest interest)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _interestService.Add(interest);

            if (interest.Id == 0) return BadRequest();

            return CreatedAtAction(nameof(GetById), interest.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterest(int id, Interest interest)
        {
            if (id != interest.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            await _interestService.Update(interest);

            return Ok(interest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterest(int id)
        {
            var interest = await _interestService.GetById(id);

            if (interest == null) return NotFound();

            await _interestService.Delete(interest);

            return Ok();
        }
    }
}
