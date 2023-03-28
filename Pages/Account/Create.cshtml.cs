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
                Message = "��ƭ��Ʒs�W";
                return Page();
            }
            Message = "����Ƥֶ�";
            return Page();
        }
    }
}
