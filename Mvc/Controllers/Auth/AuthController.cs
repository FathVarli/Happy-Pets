using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Extension;
using Mvc.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Controllers.Auth
{
    public class AuthController : Controller
    {
        IAuthService _authService;
        IForgotPasswordService _forgotPasswordService;
        IHttpContextAccessor _httpContext;

        public AuthController(IAuthService authService, IForgotPasswordService forgotPasswordService, IHttpContextAccessor httpContext)
        {
            _authService = authService;
            _forgotPasswordService = forgotPasswordService;
            _httpContext = httpContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var result = _authService.Login(loginViewModel.userLoginDto);
            if (result.Success)
            {
                var token = _authService.CreateAccessToken(result.Data);
                if (token.Success)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token.Data.Token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var jti = tokenS.Claims;

                    _httpContext.HttpContext.Session.SetObject("user", jti);
                    _httpContext.HttpContext.Session.SetInt32("userId", Convert.ToInt32(jti.First().Value));
                    return Json(new { success = true, url = "Pet/Index" });

                }


            }
            return Json(new { success = false, message = result.Message });
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel loginViewModel)
        {
            var result = _authService.Register(loginViewModel.registerDto);
            if (result.Success)
            {
                return Json(new { success = true, message = result.Message });
            }
            return Json(new { success = false, message = result.Message });
        }

        [HttpPost]
        public IActionResult ForgotPassword(LoginViewModel loginViewModel)
        {
            var result = _forgotPasswordService.PasswordResetMail(loginViewModel.forgotPasswordDto);
            if (result.Success)
            {
                return Json(new { success = true, message = result.Message });
            }
            return Json(new { success = false, message = result.Message });
        }
    }
}
