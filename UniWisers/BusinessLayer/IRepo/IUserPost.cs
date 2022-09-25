using UniWisers.Areas.Identity.Data;
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
        public Task<IEnumerable<UserPostDTO>> GetSpecificUserPostList(string id);
        public bool ReportPost(int? postId);
        public bool UpdateReportedPostStatus(int postId, string status);
        public IEnumerable<ReportedPostDTO> GetReportedPostList();
    }
}
