using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.UserVMs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookle.MVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;

        // Constructor dependency injection
        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            // Giriş edən istifadəçinin ID-sini əldə et
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Kullanıcı ID'si

            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // Giriş yapılmamışsa login sayfasına yönlendir
            }

            var userProfile = await _userService.GetUserProfileAsync(userId);  // Profil məlumatlarını əldə et
            if (userProfile == null)
            {
                return NotFound();  // İstifadəçi tapılmadıqda səhv mesajı
            }

            return View(userProfile);  // Profil məlumatlarını View-ya göndər
        }
    }
}

