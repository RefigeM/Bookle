using Bookle.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                ViewData["Name"] = model.Name;
                ViewData["Email"] = model.Email;
                ViewData["Message"] = model.Message;

                return View("MessageSent");
            }

            return View();
        }
    }
}
