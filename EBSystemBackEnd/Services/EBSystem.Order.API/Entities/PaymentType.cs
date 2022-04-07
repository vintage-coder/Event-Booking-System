using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBSystem.Order.API.Entities
{
    public class PaymentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long paymentTypeId { get; set; }


        public string? paymentTypeName { get; set; }
    }
}
