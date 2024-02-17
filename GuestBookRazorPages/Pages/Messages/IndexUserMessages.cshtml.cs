using GuestBookRazorPages.Models;
using GuestBookRazorPages.Models.AccountModels;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages.Messages
{
    public class IndexUserMessagesModel : PageModel
    {
        private readonly IMessageRepository _Repository;
        private readonly IAccountRepository _accountRepository;
        public IndexUserMessagesModel(IMessageRepository Repository, IAccountRepository accountRepository)
        {
            _Repository = Repository;
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public List<Message> Messages { get; set; } = default!;

        [BindProperty]
        public User CurrentUser { get; set; }

        public async Task<IActionResult> OnGet()
        {
            CurrentUser = await _accountRepository.GetByLogin(HttpContext.Session.GetString("Login"));
            Messages = await _Repository.GetUserMessages(CurrentUser);
            return Page();
        }
    }
}
