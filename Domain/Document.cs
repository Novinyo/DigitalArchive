using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Document: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DocumentType DocumentType { get; set; }
        public Guid DocumentTypeId { get; set; }
        public string DocumentURL { get; set; }
        public Student Student { get; set; }
        public Guid? StudentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}