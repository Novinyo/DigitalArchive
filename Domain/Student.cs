using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Student:BaseEntity
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public School School { get; set; }
        public Guid SchoolId { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public Contact Contact { get; set; }
        public bool HaveMedicalCondition { get; set; }
        public string ConditionRemarks { get; set; }
        public string Description { get; set; }

        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}