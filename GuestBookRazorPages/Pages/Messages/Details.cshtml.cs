using GuestBookRazorPages.Models;
using GuestBookRazorPages.Models.AccountModels;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages.Messages
{
    public class DetailsModel : PageModel
    {
        private readonly IMessageRepository _Repository;
        private readonly IAccountRepository _accountRepository;
        public DetailsModel(IMessageRepository Repository, IAccountRepository accountRepository)
        {
            _Repository = Repository;
            _accountRepository = accountRepository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Messages = await _Repository.GetById(id.Value);

            User user = await _accountRepository.GetById(Messages.UserId);
            Messages.User = user;

            if (Messages == null)
            {
                return NotFound();
            }

            return Page();
        }
        [BindProperty(SupportsGet = true)]
        public Message Messages { get; set; } = default!;
    }
}
