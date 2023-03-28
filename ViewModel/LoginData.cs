using System.ComponentModel.DataAnnotations;
namespace Razor_PagesMovie.ViewModel
{
    public class AccoutActionData
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "帳號")]
        public string UserName { get; set; } = null!;

        [Required]
        [Display(Name = "密碼")]
        [StringLength(12, MinimumLength =6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;

    }
}
