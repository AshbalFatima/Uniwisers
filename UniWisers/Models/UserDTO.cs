namespace UniWisers.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public string ProfilePicUrl { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}