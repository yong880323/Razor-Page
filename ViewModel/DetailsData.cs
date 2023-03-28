using System.ComponentModel.DataAnnotations;

namespace Razor_PagesMovie.ViewModel
{
    public partial class DetailsData
{
        [Key]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "帳號")]
        public string UserName { get; set; } = null!;

        [Required]
        [Display(Name = "密碼")]
        [StringLength(12, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; } = null!;

        public DateTime? UserBegindate { get; set; }
        public DateTime? UserEnddate { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        public string? UserRole { get; set; }

        public DateTime? UserBirthday { get; set; }

        public DateTime? Userctime { get; set; }
    }
}
