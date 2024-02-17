using GuestBookRazorPages.Models;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages.Messages
{
    public class DeleteModel : PageModel
    {
        private readonly IMessageRepository _Repository;
        public DeleteModel(IMessageRepository Repository)
        {
            _Repository = Repository;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _Repository.Delete(id.Value);

            return RedirectToPage("/Messages/IndexUserMessages");
        }
        [BindProperty(SupportsGet = true)]
        public Message Messages { get; set; } = default!;
    }
}
