using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Contact:BaseEntity
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int RelationshipType { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string PostalAddress { get; set; }
        
    }
}