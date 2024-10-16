using System.ComponentModel.DataAnnotations;

namespace SampleAspNetCore8WithIdentityRoleBase.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; } = null!;

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = null!;

        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "شماره تلفن")]
        public string? Phone { get; set; }

        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        public ICollection<OrderApp>? Orders { get; set; } = null!;
    }
}
