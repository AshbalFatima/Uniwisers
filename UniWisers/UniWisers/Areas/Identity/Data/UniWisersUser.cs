using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UniWisers.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UniWisersUser class
public class UniWisersUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePic { get; set; }
    public string? Job { get; set; }
    public string? Country { get; set; }
}

