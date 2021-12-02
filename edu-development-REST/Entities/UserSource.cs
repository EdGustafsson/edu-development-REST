using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Entities
{
    [Table("UserSource", Schema = "UserSources")]
    public class UserSource
    {
        /// <summary>
        /// A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// The course's Id retrieved from the external source
        /// </summary>
        /// <example>52</example>
        [Required]
        public int ExternalId { get; set; }

        /// <summary>
        /// The source of the retrieved Course
        /// </summary>
        /// <example>Learnpoint</example>
        [Required]
        public string ExternalSource { get; set; }


    }
}
