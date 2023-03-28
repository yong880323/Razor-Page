using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_PagesMovie.Models;
using Razor_PagesMovie.Util;
using Razor_PagesMovie.ViewModel;

namespace Razor_PagesMovie.Pages.Create
{
    public class CreateModel : PageModel
    {
        private readonly Accountaction _create;

        public CreateModel(Accountaction create)
        {
            _create = create;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public CreateData Create { get; set; } = null!;
        public string? Message { get; set; } 

        public async Task<IActionResult> OnPostAsync()
        {
            if (Create !=null)
            {
                if(await _create.Create(Create)) { 
                return RedirectToPage("./Index");
                }
                Message = "資料重複新增";
                return Page();
            }
            Message = "有資料少填";
            return Page();
        }
    }
}
