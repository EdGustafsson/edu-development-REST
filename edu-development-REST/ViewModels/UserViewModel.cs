using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace edu_development_REST.ViewModels
{
    public class UserViewModel
    {
        [Column(TypeName = "VARCHAR(50)")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}