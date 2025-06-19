using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineTrading.Service.DTOs.Authentication;
using OnlineTrading.Service.Interfaces;
using OnlineTrading.Service.Intterfaces;
using OnlineTrading.Web.Models.Authentication;

namespace OnlineTrading.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;

        public AccountController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(new Service.DTOs.Authentication.LoginRequest
            {
                Username = model.Username,
                Password = model.Password,
            });

            if (result.Success)
            {
                HttpContext.Session.SetInt32("UserId", result.EmployeeId.Value);
                HttpContext.Session.SetString("UserName", result.FullName);

                var user = await _userService.RetrieveById(result.EmployeeId.Value); 

                if(user.Role == "Customer")
                    return RedirectToAction("Index", "Order");
                if(user.Role == "Moderator")
                    return RedirectToAction("ModeratorIndex", "Order");
                if(user.Role == "Admin")
                    return RedirectToAction("AdminIndex", "Order");

            }

            ViewData["ErrorMessage"] = result.ErrorMessage ?? "Invalid username or password";
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.Role = "Customer";

            var registerRequest = new Service.DTOs.Authentication.RegisterRequest
            {
                Username = model.Username,
                Password = model.Password,
                FullName = model.FullName,
                Role = model.Role
            };

            var result = await _authService.RegisterAsync(registerRequest);

            if (result)
            {
                // Redirect to login or directly login the user
                return RedirectToAction("Login");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult RegisterModerator()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterModerator(RegisterViewModel model)
        {
            model.Role = "Moderator";

            var registerRequest = new Service.DTOs.Authentication.RegisterRequest
            {
                Username = model.Username,
                Password = model.Password,
                FullName = model.FullName,
                Role = model.Role
            };

            var result = await _authService.RegisterAsync(registerRequest);

            if (result)
            {
                // Redirect to login or directly login the user
                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
