using Microsoft.AspNetCore.Mvc;
using OnlineTrading.Service.Interfaces;
using OnlineTrading.Web.Models.User;

namespace OnlineTrading.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.RetrieveAll(); // Assumes you have GetAllAsync()

            var model = users.Select(u => new UserListViewModel
            {
                Id = u.UserId,
                FullName = u.FullName,
                Role = u.Role,
            }).ToList();

            return View(model);
        }
    }
}
