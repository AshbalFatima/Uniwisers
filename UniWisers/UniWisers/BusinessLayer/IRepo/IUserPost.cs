using UniWisers.Models;
namespace UniWisers.BusinessLayer.IRepo
{
    public interface IUserPost
    {
        public bool AddPost(UserPostDTO post);
        public bool EditPost(UserPostDTO post);
        public bool DeletePost(int postId);
        public UserPostDTO GetPostById(int id);
        public IEnumerable<UserPostDTO> GetUserPostList();
        public string GetUserName(string id);
    }
}
