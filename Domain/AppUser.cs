using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser: IdentityUser
    {
        public override string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public override string PhoneNumber { get; set; }
        public override string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; }
        public bool CanLogin { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}