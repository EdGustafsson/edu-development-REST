using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace edu_development_REST.ViewModels
{
    public class CourseViewModel

    {

        /// <summary>
        /// A human readable course name, using only alphanumeric characters
        /// </summary>
        /// <example>MATMAT02A-KD</example>
        [Required]
        public string CourseCode { get; set; }


        /// <summary>
        /// A human readable course name, using only alphanumeric characters
        /// </summary>
        /// <example>English 101</example>
        [Required]
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