using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniWisers.Areas.Identity.Data;
namespace UniWisers.Models
{
    public class ReportedPostDTO
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string PostData { get; set; } = default!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string postImageUrl { get; set; }
        public string Status { get; set; }

    }
}
