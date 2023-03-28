using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_PagesMovie.Models;
using Razor_PagesMovie.Util;
using Razor_PagesMovie.ViewModel;

namespace Razor_PagesMovie.Pages.Account
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly Accountaction _Details;


        public DetailsModel(Accountaction Details)
        {
            _Details = Details;
        }

        [BindProperty]
        public List<User> Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
             Users = await _Details.GetAllUsers(null);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var user in Users)
            {
                await _Details.UpdateUserAsync(user);
            }
            return RedirectToPage("Details");
        }
    }

}
