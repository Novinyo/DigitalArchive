using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Dtos
{
    public class UserStaffDto
    {
        public string Id { get; set; }
         public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
    }
}