using AutoMapper;
using CodingExam.Common;
using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExam.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestService _interestService;
        private readonly IMapper _mapper;

        public InterestsController(IInterestService interestService, IMapper mapper)
        {
            _interestService = interestService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var interests = await _interestService.GetAll();

            return Ok(_mapper.Map<List<InterestDto>>(interests));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var interest = await _interestService.GetById(id);

            if (interest == null) return NotFound();

            return Ok(_mapper.Map<InterestDto>(interest));
        }

        [HttpPost]
        public async Task<IActionResult> Add(InterestDto interestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var interest = _mapper.Map<Interest>(interestDto);

            var interestResult = await _interestService.Add(interest);

            if (interestResult == null) return BadRequest();

            return CreatedAtAction("GetById", interestResult.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, InterestDto interestDto)
        {
            if (id != interestDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            await _interestService.Update(_mapper.Map<Interest>(interestDto));

            return Ok(interestDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var interest = await _interestService.GetById(id);

            if (interest == null) return NotFound();

            await _interestService.Delete(interest);

            return Ok();
        }
    }
}
