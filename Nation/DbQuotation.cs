using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nation
{
    public class DbQuotation
    {
        [Key, Column(Order =0)]
        public string SupplierId { get; set; }

        [Key,Column(Order =1)]
        public string Product { get; set; }

        public double? CostPerUnit { get; set; } 

    }
}
