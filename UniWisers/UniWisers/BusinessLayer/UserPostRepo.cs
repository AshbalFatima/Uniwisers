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
            }
            return userPost;
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
                userPost.FirstName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).FirstName;
                userPost.LastName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).LastName;
                users.Add(userPost);
            }
            return users;
        }

        public UserDTO GetUserById(string id)
        {
            var user =  _db.Users.FirstOrDefault(i => i.Id == id);
            var userDTO = new UserDTO();
            userDTO.Id = user.Id;
            userDTO.FirstName = user.FirstName;
            userDTO.LastName = user.LastName;
            userDTO.Country = user.Country;
            userDTO.Job = user.Job;
            userDTO.ProfilePicUrl = user.ProfilePic;
            return userDTO;

        }

        public string GetUserName(string id)
        {

            var user = _db.Users.FirstOrDefault(i => i.Id == id).FirstName;
            return user + " " + _db.Users.FirstOrDefault(i => i.Id == id).LastName;
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
                userPost.postImageUrl = post.postImageUrl;
                userPost.FirstName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).FirstName;
                userPost.LastName = _db.Users.FirstOrDefault(i => i.Id == post.UserId).LastName;
                users.Add(userPost);
            }
           return users;
        }
    }
}
