using GuestBookRazorPages.Models.AccountModels;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages.Account
{
    public class RegistrationModel : PageModel
    {

        private readonly IAccountRepository _Repository;
        public RegistrationModel(IAccountRepository Repository)
        {
            _Repository = Repository;
        }

        [BindProperty]
        public SignUpViewModel Input { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Console.WriteLine("Good");
            User user = new User()
            {
                Login = Input.Login,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Password = Input.Password
            };

            await _Repository.Create(user);

            Console.WriteLine("Good");

            return RedirectToPage("/Account/Login");
        }
    }
}
