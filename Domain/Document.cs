using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Document: BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DocumentType DocumentType { get; set; }
        public Guid OwnerId { get; set; }
        public int OwnerType { get; set; }
        public string DocumentURL { get; set; }
    
    }
}