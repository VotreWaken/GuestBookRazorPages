using GuestBookRazorPages.Models;
using GuestBookRazorPages.Models.AccountModels;

namespace GuestBookRazorPages.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetUserMessages(User userSpecification);
    }
}
