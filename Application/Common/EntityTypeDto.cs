using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common
{
    public class EntityTypeDto
    {
         public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}