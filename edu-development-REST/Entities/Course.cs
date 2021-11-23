using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_development_REST.Entities
{
    [Table("Course", Schema = "Courses")]
    public class Course
    {
        /// <summary>
        /// A unique Guid id
        /// </summary>
        /// <example>bc3fd254-3eea-4da8-8f45-dbd69030c306</example>
        [Required]
        [Key]
        public Guid CourseCode { get; set; }

        /// <summary>
        /// A human readable course name, using only alphanumeric characters
        /// </summary>
        /// <example>Anders123</example>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "First name can only contain alphanumeric characters")]
        public string Name { get; set; }

        /// <summary>
        /// A DateTime, tracking what the start date of the course is
        /// </summary>
        /// <example>2021-11-11T12:00:00.0000000+01:00</example>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// A DateTime, tracking what the end date of the course is
        /// </summary>
        /// <example>2021-11-11T12:00:00.0000000+01:00</example>
        [Required]
        public DateTime EndDate { get;  set; }
    }
}
