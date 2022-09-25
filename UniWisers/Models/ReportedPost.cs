using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniWisers.Areas.Identity.Data;
namespace UniWisers.Models
{
    public class ReportedPost
    {
        public int Id { get; set; }
        public string Status { get; set; } = default!;
        [ForeignKey("post")]
        public int postId { get; set; } = default!;
        public virtual UserPost post { get; set; }
    }
}
