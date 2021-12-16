using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace edu_development_REST.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// A human readable first name, using only alphanumeric characters
        /// </summary>
        /// <example>Anders</example>
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "First name can only contain alphanumeric characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// A human readable last name, using only alphanumeric characters
        /// </summary>
        /// <example>Andersson</example>
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "Last name can only contain alphanumeric characters")]
        public string LastName { get; set; }

        /// <summary>
        /// An email address
        /// </summary>
        /// <example>Anders@gmail.com</example>
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}