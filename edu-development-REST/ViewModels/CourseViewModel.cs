using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace edu_development_REST.ViewModels
{
    public class CourseViewModel

    {   /// <summary>
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
        public DateTime EndDate { get; set; }
    }
}