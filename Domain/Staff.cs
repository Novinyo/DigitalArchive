using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public StaffType StaffType { get; set; }
        public string Title { get; set; }
        public School School { get; set; }
        public Guid SchoolId { get; set; }
        public AppUser User { get; set; }
        public string PostalAddress { get; set; }
        public string StreetAddress { get; set; }
        public bool HaveMedicalCondition { get; set; }
        public string ConditionRemarks { get; set; }
        public string Description { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}