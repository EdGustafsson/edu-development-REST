using edu_development_REST.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_development_REST.Entities
{
    [Table("CourseMembership", Schema = "CourseMemberships")]
    public class CourseMembership
    {
        /// <summary>
        /// A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///  A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        public Guid CourseId { get; set; }

        /// <summary>
        /// A datetime for the date enrolled.
        /// </summary>
        /// <example>2021-11-11T12:00:00.0000000+01:00</example>
        [Required]
        public DateTime EnrolledDate { get; set; }

    }
}
