using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SampleAspNetCore8WithIdentityRoleBase.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "نام محصول")]
        public string Name { get; set; } = null!;

        [Display(Name = "قیمت")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [Display(Name = "سفارش‌ها")]
        public ICollection<OrderApp>? Orders { get; set; } = null!;
    }
}
