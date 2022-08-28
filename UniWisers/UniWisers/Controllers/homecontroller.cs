using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UniWisers.BusinessLayer.IRepo;
using UniWisers.Models;

namespace UniwisersApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserPost _userPost;
        private readonly IWebHostEnvironment Environment;

        public HomeController(IUserPost userPost, IWebHostEnvironment environment)
        {
            _userPost = userPost;
            Environment = environment;  
        }

        [HttpGet]
        public IActionResult AllPosts()
        {
            var posts = _userPost.GetUserPostList();
            return View();
        }
      
        [HttpGet]
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var AllPosts = _userPost.GetUserPostList();
                ViewBag.model = AllPosts;
                ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewBag.CurrentUserName = _userPost.GetUserName(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View();
            }
            return Redirect("~/Identity/Account/Login");
        }


        [HttpPost]
        public IActionResult AddUserPost(UserPostDTO userData)
        {
            if (userData != null)
            {
                if(userData.postImage != null)
                {
                    string wwwPath = this.Environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var fileName = Path.GetFileName(userData.postImage.FileName);
                    var pathWithFileName = Path.Combine(path, fileName);
                    using (FileStream stream = new FileStream(pathWithFileName, FileMode.Create))
                    {
                        userData.postImage.CopyTo(stream);
                    }
                }
                userData.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var AddData = _userPost.AddPost(userData);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditUserPost(int id)
        {
            var userPost = _userPost.GetPostById(id);
            return View(userPost);
        }

        [HttpPost]
        public IActionResult EditUserPost(UserPostDTO editedUserPost)
        {
            var editPost = _userPost.EditPost(editedUserPost);
            return RedirectToAction("AddUserPost");
        }

        public IActionResult DeletetUserPost(int id)
        {
            var deletePost = _userPost.DeletePost(id);
            return RedirectToAction("Index");
        }

    }
}