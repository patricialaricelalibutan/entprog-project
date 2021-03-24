using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINALTEST1.Models
{
    public class Monetary
    {
        [Key]
        public int MonetaryID { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserID { get; set; }

        [Range(1.00, 1000000000.00, ErrorMessage = "Please fill in the valid number.")]
        [Required(ErrorMessage = "Required.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Validate { get; set; }

        //Navigation property
        public User User { get; set; }
    }
}
