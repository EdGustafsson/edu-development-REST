using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.ViewModels
{
    public class CourseSourceViewModel
    {
        public Guid CourseId { get; set; }
        public int ExternalId { get; set; }
        public string ExternalSource { get; set; }
    }
}
