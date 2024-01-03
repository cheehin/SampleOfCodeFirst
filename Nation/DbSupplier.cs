using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nation
{
    [Table("DbSupplier")]
    public class DbSupplier
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [MaxLength(2)]
        public string CountryCode { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
