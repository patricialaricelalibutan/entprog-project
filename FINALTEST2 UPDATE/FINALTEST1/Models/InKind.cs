using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINALTEST1.Models
{
    public class InKind
    {
        [Key]
        public int InKindID { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserID { get; set; }

        //[ForeignKey("Transaction")]
        //public int? TransactionID { get; set; }

        [Required]
        public string Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Date { get; set; }

        //Navigation property
        public User User { get; set; }

        public Transaction Transaction { get; set; }
    }
}