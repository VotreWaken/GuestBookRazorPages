using GuestBookRazorPages.Models;
using GuestBookRazorPages.Models.AccountModels;
using Microsoft.EntityFrameworkCore;

namespace GuestBookRazorPages.Repository
{
    public class MessageRepository : IMessageRepository
    {

        // Context
        private readonly GuestBookContext _context;

        // Constructor
        public MessageRepository(GuestBookContext context)
        {
            _context = context;
        }

        // Get All Messages
        public async Task<List<Message>> GetAll()
        {
            return await _context.Messages.Include(m => m.User).ToListAsync();
        }

        // Get Message
        public async Task<Message> GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Invalid id value");
            }
            return await _context.Messages.FindAsync(id);
        }

        // Create Message

        public async Task<Message> Create(Message msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg), "Message cannot be null.");
            }

            await _context.AddAsync(msg);
            await _context.SaveChangesAsync();

            return msg;
        }

        // Update Message
        public async Task Update(Message msg)
        {
            _context.Update(msg);
            await _context.SaveChangesAsync();
        }

        // Delete Message
        public async Task Delete(int id)
        {
            var message = await GetById(id);

            if (message != null)
                _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        // Get User Messages
        public async Task<List<Message>> GetUserMessages(User concreteUser)
        {
            var myDbContext = _context.Messages.Where(m => m.User.Id == concreteUser.Id);
            return await myDbContext.ToListAsync();
        }
    }
}
