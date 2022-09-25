using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UniWisers.BusinessLayer.IRepo;
using UniWisers.Models;

namespace UniwisersApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IWebHostEnvironment Environment;

        public UserController(IUserRepo userRepo, IWebHostEnvironment environment)
        {
            _userRepo = userRepo;
            Environment = environment;  
        }

        public IActionResult Profile()
        {
            ViewBag.User = _userRepo.GetUserById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View();
        }

        [HttpGet]
        public IActionResult UserSettings()
        {
            var user = _userRepo.GetUserById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(user);
        }
        [HttpPost]
        public IActionResult UserSettings(UserDTO user)
        {
            if (user != null)
            {
                if (user.ProfileImage != null)
                {
                    string wwwPath = this.Environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var fileName = Path.GetFileName(user.ProfileImage.FileName);
                    var pathWithFileName = Path.Combine(path, fileName);
                    using (FileStream stream = new FileStream(pathWithFileName, FileMode.Create))
                    {
                        user.ProfileImage.CopyTo(stream);
                    }
                }
                user.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var AddData = _userRepo.UpdateUserDetails(user);
            }
            return RedirectToAction("UserSettings");
        }
    }
}