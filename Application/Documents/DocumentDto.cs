using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Documents
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string DocName { get; set; }
        public string DocDesc { get; set; }
        public string DocUrl { get; set; }
        public Guid DocTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}