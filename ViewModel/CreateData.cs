using Razor_PagesMovie.Models;
using System.ComponentModel.DataAnnotations;
namespace Razor_PagesMovie.ViewModel
{
    public partial class CreateData
    {
        [Required]
        [Display(Name = "帳號")]
        public string UserName { get; set; } = null!;

        [Required]
        [Display(Name = "密碼")]
        [StringLength(12, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        /* public DateTime? UserBegindate { get; set; }

        public DateTime? UserEnddate { get; set; }


        public string? UserRole { get; set; }*/
    }

}
