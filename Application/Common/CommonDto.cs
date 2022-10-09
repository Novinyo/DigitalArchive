using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common
{
    public class CommonDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public bool Active { get; set; }
    }
}