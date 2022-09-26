using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Staffs.Dtos
{
    public class StaffWDto
    {
         public string Code { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public Guid StaffTypeId { get; set; }
        public Guid SchoolId { get; set; }
        public UserWDto User { get; set; }
        public string PostalAddress { get; set; }
        public string StreetAddress { get; set; }
        public bool HaveMedicalCondition { get; set; }
        public string ConditionRemarks { get; set; }
        public string Description { get; set; }
    }
}