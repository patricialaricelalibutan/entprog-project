using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINALTEST1.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 2)]
        [Required(ErrorMessage = "Required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 2)]
        [Required(ErrorMessage = "Required.")]
        public string LastName { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required(ErrorMessage = "Required.")]
        public string City { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Required.")]
        public string Email { get; set; }
    }
}
