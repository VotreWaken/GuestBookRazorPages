using GuestBookRazorPages.Models;
using GuestBookRazorPages.Models.AccountModels;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuestBookRazorPages.Pages.Messages
{
    public class CreateModel : PageModel
    {
        private readonly IMessageRepository _Repository;
        private readonly IAccountRepository _accountRepository;
        public CreateModel(IMessageRepository Repository, IAccountRepository AccountRepository)
        {
            _Repository = Repository;
            _accountRepository = AccountRepository;
        }

        [BindProperty]
        public Message Messages { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User user = await _accountRepository.GetByLogin(HttpContext.Session.GetString("Login"));
            Messages.User = user;

            await _Repository.Create(Messages);

            return RedirectToPage("/Messages/Index");
        }
    }
}
