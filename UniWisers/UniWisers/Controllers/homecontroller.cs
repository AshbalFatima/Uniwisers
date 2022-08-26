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

        public HomeController(IUserPost userPost)
        {
            _userPost = userPost;
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
            var AllPosts = _userPost.GetUserPostList();
            ViewBag.model = AllPosts;
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.CurrentUserName = _userPost.GetUserName(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View();
        }

        [HttpPost]
        public IActionResult AddUserPost(UserPostDTO userData)
        {
            if (userData != null)
            {
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