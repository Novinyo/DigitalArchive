using System;

namespace Domain
{
    public class StaffType: BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public School School { get; set; }
        public Guid? SchoolId { get; set; }
    }
}