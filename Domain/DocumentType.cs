using System;

namespace Domain
{
    public class DocumentType:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
    }
}