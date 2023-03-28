using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_PagesMovie.Util;
using Razor_PagesMovie.ViewModel;

namespace Razor_PagesMovie.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly Accountaction _login;

        public IndexModel(Accountaction login )
        { 
            _login= login;
        }
        private void CreateUserIdCookie(int userId)
        {
            CookieOptions option = new();
            Response.Cookies.Append("userId", userId.ToString(), option);
        }
        // 登入頁面
        public IActionResult OnGet(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return Page();
        }
        [BindProperty]
        public AccoutActionData Login { get; set; } = null!;
        public string? Message { get; set; }


        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {   if(Login !=null)
            { 
            if (await _login.Login(Login))
                {
                    CreateUserIdCookie(Login.UserId);
                    if (returnUrl != null) {
                        return Redirect(returnUrl);
                    }
                    else
                    return Redirect("index");
                }
                else
                Message = "登入失敗";
            }
            return Page();
        }
    }
}
