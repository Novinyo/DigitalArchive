using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Documents.Type
{
    public class DocumentTypeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char Category { get; set; }
        public bool Active { get; set; }
    }
}