using Microsoft.AspNetCore.Mvc;
using System;
using UniWisers.BusinessLayer.IRepo;

namespace UniWisers.ViewComponents
{
    [ViewComponent(Name = "UserPosts")]
    public class UserPostsViewComponent: ViewComponent
    {
        private readonly IUserPost _userpost;

        public UserPostsViewComponent(IUserPost userpost)
        {
            _userpost = userpost;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var res = userId == "" ? _userpost.GetUserPostList(): await _userpost.GetSpecificUserPostList(userId);
            return View(res);
        }
    }
} 
