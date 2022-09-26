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
        public StaffType StudentType { get; set; }
        public Guid StudentTypeId { get; set; }
        public int Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public School School { get; set; }
        public Guid SchoolId { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public Contact Contact { get; set; }
    }
}