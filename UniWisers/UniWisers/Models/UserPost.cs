using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniWisers.Areas.Identity.Data;
namespace UniWisers.Models
{
    public class UserPost
    {
        public int Id { get; set; }
        public string PostData { get; set; } = default!;
        [ForeignKey("User")]
        public string UserId { get; set; } = default!;
        public virtual UniWisersUser User { get; set; }
        public string postImageUrl { get; set; }
        [NotMapped]
        public IFormFile postImage { get; set; }
    }
}
