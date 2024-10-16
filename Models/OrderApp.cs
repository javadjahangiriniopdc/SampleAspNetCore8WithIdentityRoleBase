using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SampleAspNetCore8WithIdentityRoleBase.Models
{
    public class OrderApp
    {
        public int Id { get; set; }

        [Display(Name = "شناسه مشتری")]
        public int CustomerID { get; set; }

        [Display(Name = "مشتری")]
        public Customer? Customer { get; set; } = null!;

        [Display(Name = "شناسه محصول")]
        public int ProductID { get; set; }

        [Display(Name = "محصول")]
        public Product? Product { get; set; } = null!;

        [Display(Name = "تعداد")]
        public int Quantity { get; set; }

        [Display(Name = "قیمت")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [Display(Name = "قیمت کل")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PriceAll { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatAt { get; set; }

        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime UpdateAt { get; set; }
    }
}
