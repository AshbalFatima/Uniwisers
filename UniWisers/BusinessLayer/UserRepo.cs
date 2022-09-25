using UniWisers.Areas.Identity.Data;
using UniWisers.BusinessLayer.IRepo;
using UniWisers.Data;
using UniWisers.Models;


namespace UniWisers.BusinessLayer
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _db;
        
        public UserRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ChangeStatus(string userID, string status)
        {
            var user = _db.Users.FirstOrDefault(i => i.Id == userID);
            if (user != null)
            {
                user.Status = status;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public UserDTO GetUserById(string id)
        {
            var user =  _db.Users.FirstOrDefault(i => i.Id == id);
            var userDTO = new UserDTO();
            userDTO.Id = user.Id;
            userDTO.FirstName = user.FirstName;
            userDTO.LastName = user.LastName;
            userDTO.Country = user.Country;
            userDTO.Status = user.Status;
            userDTO.Email = user.Email;
            userDTO.Job = user.Job;
            userDTO.ProfilePicUrl = user.ProfilePic;
            return userDTO;

        }

        public string GetUserName(string id)
        {

            var user = _db.Users.FirstOrDefault(i => i.Id == id).FirstName;
            return user + " " + _db.Users.FirstOrDefault(i => i.Id == id).LastName;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = new List<UserDTO>();
            foreach (var user in _db.Users)
            {
                users.Add(GetUserById(user.Id));
            }
            return users;
        }

        public bool UpdateUserDetails(UserDTO user)
        {
            var findUser = _db.Users.FirstOrDefault(i => i.Id == user.Id);
            if (findUser != null)
            {
                findUser.Id = user.Id;
                findUser.FirstName = user.FirstName;
                findUser.LastName = user.LastName;
                findUser.Country = user.Country;
                findUser.Email = user.Email;
                findUser.Job = user.Job;
                if (user.ProfileImage == null )
                {
                    findUser.ProfilePic = "";
                }
                else
                {
                    findUser.ProfilePic = user.ProfileImage.FileName;
                }

                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
