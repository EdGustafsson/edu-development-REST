using edu_development_REST.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace edu_development_REST.Entities
{
    [Table("CourseSource", Schema = "CourseSources")]
    public class CourseSource
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
        public Guid CourseId { get; set; }

        /// <summary>
        /// The course's Id retrieved from the external source
        /// </summary>
        /// <example>52</example>
        public int ExternalId { get; set; }

        /// <summary>
        /// The source of the retrieved Course
        /// </summary>
        /// <example>Learnpoint</example>
        public string ExternalSource { get; set; }
    }
}
