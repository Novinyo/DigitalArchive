using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Schools
{
    public class SchoolDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid SchoolTypeId { get; set; }
        public string SchoolTypeName { get; set; }
        public bool Active { get; set; }
    }
}