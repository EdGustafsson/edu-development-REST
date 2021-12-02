using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.ViewModels
{
    public class UserSourceViewModel
    {
        public Guid UserId { get; set; }
        public int ExternalId { get; set; }
        public string ExternalSource { get; set; }
    }
}
