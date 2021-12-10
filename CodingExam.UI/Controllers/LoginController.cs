using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodingExam.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;

        public LoginController(IAuthService authService,
            IUserService userService,
            IUserTokenService userTokenService)
        {
            _authService = authService;
            _userService = userService;
            _userTokenService = userTokenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("Username,Password")] LoginViewModel loginVm)
        {
            if (ModelState.IsValid)
            {
                if (await _authService.ValidateLogin(new User { Username = loginVm.Username, Password = loginVm.Password }))
                {
                    var user = await _userService.GetByUsername(loginVm.Username);

                    var accessToken = await _authService.GenerateAccessToken(loginVm.Username);
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

                    HttpContext.Session.SetString("AccessToken", accessToken);
                    HttpContext.Session.SetString("RefreshToken", refreshToken.RefreshToken);

                    return RedirectToAction("Index", "Interests", new { id = user.Interest?.Id });
                }
                else
                    return Unauthorized();
            }

            return View(loginVm);
        }
    }
}
