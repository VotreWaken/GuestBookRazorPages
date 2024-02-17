using GuestBookRazorPages.Models.AccountModels;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _Repository;
        public LoginModel(IAccountRepository Repository)
        {
            _Repository = Repository;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginViewModel Input { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Hello");
            if ((await _Repository.GetAll()).Count == 0)
                return RedirectToAction("Regist", "Account");

            if (!ModelState.IsValid)
                return BadRequest("Incorrect login or password!");

            User user = await _Repository.GetByLogin(Input.Login);


            // Check password here
            if (await _Repository.ValidatePassword(user, Input.Password))
            {
                HttpContext.Session.SetString("FirstName", user.FirstName ?? string.Empty);
                HttpContext.Session.SetString("LastName", user.LastName ?? string.Empty);
                HttpContext.Session.SetString("Login", user.Login);
            }

            return Page();
        }
    }
}
