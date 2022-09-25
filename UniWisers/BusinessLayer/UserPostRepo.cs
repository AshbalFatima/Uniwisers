using Microsoft.Extensions.Hosting;
using UniWisers.Areas.Identity.Data;
using UniWisers.BusinessLayer.IRepo;
using UniWisers.Data;
using UniWisers.Models;


namespace UniWisers.BusinessLayer
{
    public class UserPostRepo : IUserPost
    {
        private readonly ApplicationDbContext _db;
        
        public UserPostRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool AddPost(UserPostDTO post)
        {
            var userPost = new UserPost();
            if (post != null)
            {
                userPost.UserId = post.UserId;
                userPost.PostData = post.PostData;
                if(post.postImage != null)
                {
                    userPost.postImageUrl = post.postImage.FileName;
                }
                else
                {
                    userPost.postImageUrl = "";
                }
                userPost.Status = Status.Post_Active;
                _db.UserPosts.Add(userPost);
                _db.SaveChanges();
                return true;    
            }
            return false;
        }

        public bool DeletePost(int postID)
        {
            var post = _db.UserPosts.FirstOrDefault(x => x.Id == postID);
            if (post != null)
            {
                _db.Remove(post);
                _db.SaveChanges();
                return true;
            }
            return false;

        }

        public bool EditPost(UserPostDTO post)
        {
            var userPost = new UserPostDTO();
            var oldPost = _db.UserPosts.FirstOrDefault(x => x.Id == post.Id);
            if (oldPost != null)
            {
                oldPost.PostData = post.PostData;
                oldPost.Status = post.Status;
                _db.SaveChanges();
                return true;
            }
            return false;

        }

        public UserPostDTO GetPostById(int id)
        {
            var userPost = new UserPostDTO();
            var post = _db.UserPosts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                userPost.Id = post.Id;
                userPost.UserId = post.UserId;
                userPost.PostData = post.PostData;
                userPost.Status = post.Status;
            }
            return userPost;
        }

        public IEnumerable<ReportedPostDTO> GetReportedPostList()
        {
            var postList = new List<ReportedPostDTO>();
            foreach (var post in _db.ReportedPosts)
            {
                var reportedPost = new ReportedPostDTO();
                reportedPost.PostId = post.postId;
                var UserID = _db.UserPosts.FirstOrDefault(i=>i.Id == post.postId).UserId;
                reportedPost.UserId = UserID;
                reportedPost.Status = post.Status;
                reportedPost.FirstName = _db.Users.FirstOrDefault(i => i.Id == UserID).FirstName;
                reportedPost.LastName = _db.Users.FirstOrDefault(i => i.Id == UserID).LastName;
                reportedPost.postImageUrl = _db.UserPosts.FirstOrDefault(i => i.Id == post.postId).postImageUrl;
                reportedPost.PostData = _db.UserPosts.FirstOrDefault(i => i.Id == post.postId).PostData;
                postList.Add(reportedPost);
            }
            return postList;
        }

        public async Task<IEnumerable<UserPostDTO>> GetSpecificUserPostList(string id)
        {
            var users = new List<UserPostDTO>();
            foreach (var post in _db.UserPosts.OrderByDescending(i => i.Id).Where(x => x.UserId == id))
            {
                var userPost = new UserPostDTO();
                userPost.Id = post.Id;
                userPost.UserId = post.UserId;
                userPost.PostData = post.PostData;
                userPost.postImageUrl = post.postImageUrl;
                userPost.Status = post.Status;
                userPost.FirstName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).FirstName;
                userPost.LastName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).LastName;
                userPost.UserImageUrl = _db.Users.FirstOrDefault(i => i.Id == post.UserId).ProfilePic;
                users.Add(userPost);
            }
            return users;
        }

        public IEnumerable<UserPostDTO> GetUserPostList()
        {
            var users = new List<UserPostDTO>();
            foreach (var post in _db.UserPosts.OrderByDescending(i=>i.Id))
            {
                var userPost = new UserPostDTO();
                userPost.Id = post.Id;
                userPost.UserId = post.UserId;
                userPost.PostData = post.PostData;
                userPost.Status = post.Status;
                userPost.postImageUrl = post.postImageUrl;
                userPost.FirstName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).FirstName;
                userPost.LastName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).LastName;
                userPost.UserImageUrl = _db.Users.FirstOrDefault(i => i.Id == post.UserId).ProfilePic;
                users.Add(userPost);
            }
           return users;
        }

        public bool ReportPost(int? postId)
        {
            var reportPost = new ReportedPost();
            if (postId != null )
            {
                reportPost.postId = (int)postId;
                reportPost.Status = Status.Reported_Post_UnderReview;
                _db.ReportedPosts.Add(reportPost);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateReportedPostStatus(int postId, string status)
        {
            var getReportedPost = _db.ReportedPosts.FirstOrDefault(i=>i.postId == postId);
            if (getReportedPost != null)
            {
                getReportedPost.Status = status;
                if (status == Status.Reported_Post_Approved)
                {
                    UpdatePostStatus(postId, Status.Post_Hide);
                }
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdatePostStatus(int postId, string status)
        {
            var getPost = _db.UserPosts.FirstOrDefault(i => i.Id == postId);
            if (getPost != null)
            {
                getPost.Status = status;
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
