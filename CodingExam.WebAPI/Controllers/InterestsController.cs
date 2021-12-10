using AutoMapper;
using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.WebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExam.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestService _interestService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public InterestsController(IInterestService interestService, IUserService userService, IMapper mapper)
        {
            _interestService = interestService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var interests = await _interestService.GetAll();

                return Ok(_mapper.Map<List<InterestDto>>(interests));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            try
            {
                var user = await _userService.GetById(id);

                if (user == null) return BadRequest();

                var interest = await _interestService.GetByUserId(id);

                if (interest == null) return NotFound();

                return Ok(_mapper.Map<InterestDto>(interest));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(InterestDto interestDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var interest = _mapper.Map<Interest>(interestDto);

                var interestResult = await _interestService.Add(interest);

                if (interestResult == null) return BadRequest();

                return CreatedAtAction("GetByUserId", interestResult.UserId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, InterestDto interestDto)
        {
            try
            {
                if (id != interestDto.Id) return BadRequest();

                if (!ModelState.IsValid) return BadRequest();

                await _interestService.Update(_mapper.Map<Interest>(interestDto));

                return Ok(interestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var interest = await _interestService.GetById(id);

                if (interest == null) return NotFound();

                await _interestService.Delete(interest);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
