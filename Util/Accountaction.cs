using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using Razor_PagesMovie.Models;
using Razor_PagesMovie.Pages.Account;
using Razor_PagesMovie.ViewModel;
using System.Linq;
using System.Security.Claims;

namespace Razor_PagesMovie.Util
{
    public class Accountaction
    {
        #region 資料庫連線
        private readonly DBContext _context;
        private readonly IHttpContextAccessor _auth;

        public Accountaction(DBContext context, IHttpContextAccessor auth)
        {
            _context = context;
            _auth = auth;
        }
        #endregion

        #region 使用者登入
        public bool IsUserRegistered(AccoutActionData Login)
        {
            if (_context.Users.Any(a => a.UserName == Login.UserName && a.UserPassword == Login.Password))
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<bool> Login(AccoutActionData account)
        {
            if (IsUserRegistered(account))
            {

                /* account!.Role = _context.Users
                    .Where(c => c.UserName == account.UserName)
                    .Select(c => c.UserRole)
                    .FirstOrDefault();*/
                account!.Role = _context.Users 
                                .FirstOrDefault(c => c.UserName == account.UserName)
                                .UserRole;

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,account.UserName),
                    new Claim(ClaimTypes.Role,account.Role)
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var ClaimsPrincipal = new ClaimsPrincipal(claimIdentity);
                await _auth.HttpContext!.SignInAsync(ClaimsPrincipal);
                account.UserId =  GetUserById(account);
                return true;
            }
            return false;
        }
        #endregion

        #region 抓取使用者ID
        public  int GetUserById(AccoutActionData account = null!)
        {
            account!.UserId = _context.Users
                            .FirstOrDefault(a => a.UserName == account.UserName && a.UserPassword == account.Password)
                            .UserId;
            return (account.UserId);
                }
        #endregion

        #region 使用者新增
        public async Task<bool> Create( CreateData create)
        { 
                if (create == null)
                {
                    return false;
                }

            /*if (_context.Users.Any(a => a.UserName == create.UserName && a.UserPassword == create.UserPassword))
            {
                return false;
            }*/
            var existingUser = await _context.Users
                    .SingleOrDefaultAsync(a => a.UserName == create.UserName && a.UserPassword == create.UserPassword);

                if (existingUser != null)
                {
                    return false;
                }
                var newUser = new User
                {
                    UserName = create.UserName,
                    UserPassword = create.UserPassword,
                    UserEmail = create.UserEmail,
                    UserBegindate = DateTimeOffset.UtcNow.DateTime
                };

                await _context.Users.AddAsync(newUser);

                /* await _context.Users.AddAsync(new User{UserName= create.UserName,UserPassword = create.UserPassword,UserEmail = create.UserEmail,UserBegindate = DateTime.Now} );*/
                await _context.SaveChangesAsync();
                return true;
        }
        #endregion

        #region 抓取所有使用者資料
        public async Task<List<User>>  GetAllUsers(int? userId )
        {
            if (userId !=null)
            {
                var users = await _context.Users
                    .Where(u => u.UserId != userId)
                    .ToListAsync();
                return users;
            }
            return await _context.Users.ToListAsync();
        }
        #endregion

        #region 修改使用者資料

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(m => m.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.UserEmail = user.UserEmail;
                //existingUser.UserPassword = user.UserPassword;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        #endregion


        #region 刪除使用者資料
        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = _context.Users.FirstOrDefault(m => m.UserId == id);

            if (existingUser != null)
            {
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        #endregion
    }

}

