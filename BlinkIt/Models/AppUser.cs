
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace BlinkIt.Models
{
    public class AppUser:IdentityUser
    {
        
        public string? Street { get; set; }
        
        public string? City { get; set; }    
      
        public string? Country { get; set; }

        public string? ProfileImg { get; set; }
    }
}
