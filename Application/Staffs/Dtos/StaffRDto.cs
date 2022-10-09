using System;
using System.Collections.Generic;
using Application.Authentication;
using Application.Common.Dtos;
using Application.Staffs.Dtos;

namespace Application.Staffs
{
    public class StaffRDto
    {
         public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public string StaffTypeName { get; set; }
        public Guid StaffTypeId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public Guid SchoolId { get; set; }
       public string UserId { get; set; }
       public string Avatar { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public string PostalAddress { get; set; }
        public string StreetAddress { get; set; }
        public bool HaveMedicalCondition { get; set; }
        public string ConditionRemarks { get; set; }
        public string Description { get; set; }
    }
}