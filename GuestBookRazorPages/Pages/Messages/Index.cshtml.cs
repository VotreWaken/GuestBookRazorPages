using GuestBookRazorPages.Models;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly IMessageRepository _Repository;
        public IndexModel(IMessageRepository Repository)
        {
            _Repository = Repository;
        }

        [BindProperty]
        public List<Message> Messages { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            Messages = await _Repository.GetAll();
            return Page();
        }
    }
}
