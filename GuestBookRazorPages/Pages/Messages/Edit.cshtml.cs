using GuestBookRazorPages.Models;
using GuestBookRazorPages.Models.AccountModels;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages.Messages
{
    public class EditModel : PageModel
    {
        private readonly IMessageRepository _Repository;
        private readonly IAccountRepository _accountRepository;
        public EditModel(IMessageRepository Repository, IAccountRepository accountRepository)
        {
            _Repository = Repository;
            _accountRepository = accountRepository;
        }

        [BindProperty(SupportsGet = true)]
        public Message Messages { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await _accountRepository.GetByLogin(HttpContext.Session.GetString("Login"));
            Message mes = new Message()
            {
                Id = Messages.Id,
                Mesage = Messages.Mesage,
                User = user,
                UserId = Messages.UserId,
            };

            if (Messages == null)
            {
                return NotFound();
            }

            await _Repository.Update(mes);

            return RedirectToPage("/Messages/IndexUserMessages");
        }
    }
}
