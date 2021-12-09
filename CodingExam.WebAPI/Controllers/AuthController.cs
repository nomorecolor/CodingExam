using AutoMapper;
using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.WebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodingExam.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IMapper _mapper;

        public AuthController(IAuthService userTokenService, IMapper mapper)
        {
            _authService = userTokenService;

            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] AuthDto auth)
        {
            if (await _authService.ValidateLogin(new User { Username = auth.Username, Password = auth.Password }))
                return Ok();
            else
                return Unauthorized();
        }
    }
}
