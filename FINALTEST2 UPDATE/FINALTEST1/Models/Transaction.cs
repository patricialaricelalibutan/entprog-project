using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINALTEST1.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [ForeignKey("InKind")]
        public int? InKindID { get; set; }

        public string Item { get; set; }

        public int? Quantity { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal? UnitCost { get; set; }

        [Display(Name = "Total Cost")]
        public decimal? TotalCost { get; set; }

        [Required]
        public bool Out { get; set; }

        //Navigation property
        public InKind InKind { get; set; }
    }
}
