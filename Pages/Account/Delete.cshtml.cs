using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_PagesMovie.Models;
using Razor_PagesMovie.Util;

namespace Razor_PagesMovie.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly Accountaction _Delete;

        public DeleteModel(Accountaction Delete)
        {
            _Delete = Delete;
        }

        [BindProperty]
      public List<User> Users { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            int.TryParse(Request.Cookies["userid"], out int userId);

            Users = await _Delete.GetAllUsers(userId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int[] ids)
        {
            if (ids == null)
            {
                return NotFound();
            }

            foreach (var id in ids)
            {
                await _Delete.DeleteUserAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
