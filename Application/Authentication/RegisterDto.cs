using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class RegisterDto
    {
       
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-z]).{6,12}$", ErrorMessage = "Password must be complex")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-z]).{6,12}$", ErrorMessage = "Password must be complex")]
        public string MatchPassword { get; set; }
    }
}