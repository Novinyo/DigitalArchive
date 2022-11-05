using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Students
{
    public class StudentWDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public Guid SchoolId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public ContactDto Contact { get; set; }
         public bool HaveMedicalCondition { get; set; }
        public string ConditionRemarks { get; set; }
        public string Description { get; set; }
    }
}