namespace UniWisers.Models
{
    public class UserPostDTO
    {
        public int Id { get; set; }
        public string PostData { get; set; } = default!;
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}