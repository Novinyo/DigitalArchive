using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Students
{
    public class ContactDto
    {
        public Guid Id { get; set; }
         public string FatherFirstName { get; set; }
        public string FatherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherLastName { get; set; }
        public string FatherPhoneNumber { get; set; }
        public string MotherPhoneNumber { get; set; }
        public string MotherEmail { get; set; }
        public string FatherEmail { get; set; }
        public string EmergencyContact { get; set; }
        public string StreetAddress { get; set; }
        public string PostalAddress { get; set; }
    }
}