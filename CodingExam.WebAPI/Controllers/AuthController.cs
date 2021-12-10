using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.WebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodingExam.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;


        public AuthController(IAuthService authService,
            IUserService userService,
            IUserTokenService userTokenService)
        {
            _authService = authService;
            _userService = userService;
            _userTokenService = userTokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] AuthDto auth)
        {
            try
            {
                if (await _authService.ValidateLogin(new User { Username = auth.Username, Password = auth.Password }))
                {
                    var user = await _userService.GetByUsername(auth.Username);

                    var accessToken = await _authService.GenerateAccessToken(auth.Username);
                    var refreshToken = await _userTokenService.GetByUserId(user.Id);

                    if (refreshToken == null)
                    {
                        refreshToken = new UserToken();
                        refreshToken.RefreshToken = await _authService.GenerateRefreshToken();
                        refreshToken.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                    }

                    var userToken = new UserToken
                    {
                        UserId = user.Id,
                        RefreshToken = refreshToken.RefreshToken,
                        RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime
                    };

                    await _userTokenService.Add(userToken);

                    return Ok(new { CurrentUser = new { Id = user.Id, Username = user.Username }, AccessToken = accessToken, refreshToken.RefreshToken });
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] AuthDto auth)
        {
            try
            {
                var token = await _userTokenService.GetByToken(auth.RefreshToken);

                if (token != null)
                {
                    var accessToken = await _authService.GenerateAccessToken(token.User.Username);
                    token = await _userTokenService.Update(token);

                    return Ok(new { AccessToken = accessToken, token.RefreshToken });
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
