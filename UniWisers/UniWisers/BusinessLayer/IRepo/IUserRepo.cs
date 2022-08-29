using UniWisers.Areas.Identity.Data;
using UniWisers.Models;
namespace UniWisers.BusinessLayer.IRepo
{
    public interface IUserRepo
    {
        public bool UpdateUserDetails(UserDTO user);
        public string GetUserName(string id);
        public UserDTO GetUserById(string id);
    }
}
