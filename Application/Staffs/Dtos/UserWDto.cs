using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Staffs.Dtos
{
    public class UserWDto
    {
        public string UserId { get; set; }
        public string Avatar { get; set; }
        [Required]
        public string Username { get; set; }
         [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}