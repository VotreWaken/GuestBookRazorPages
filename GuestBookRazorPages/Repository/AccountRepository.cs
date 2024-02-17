using GuestBookRazorPages.Models;
using GuestBookRazorPages.Models.AccountModels;
using GuestBookRazorPages.Services;
using Microsoft.EntityFrameworkCore;

namespace GuestBookRazorPages.Repository
{
    public class AccountRepository : IAccountRepository
    {
        // Hashing Service
        private readonly IHash _hashService = new Hashing();

        // Context
        private readonly GuestBookContext _context;

        // Constructor
        public AccountRepository(GuestBookContext context)
        {
            _context = context;
        }

        // Get All Users
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.Include(u => u.Messages).ToListAsync();
        }

        // Get User By Id
        public async Task<User> GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Invalid id value");
            }
            return await _context.Users.FindAsync(id);
        }

        // Get User By Login 
        public async Task<User> GetByLogin(string login)
        {
            if (login == null)
            {
                throw new ArgumentException("Invalid login value");
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        // Create User
        public async Task<User> Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            await HashUserPassword(user);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // Update User
        public async Task Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        // Delete User
        public async Task Delete(int id)
        {
            var User = await GetById(id);

            if (User != null)
                _context.Users.Remove(User);
        }

        // Hash Password 
        public async Task HashUserPassword(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentNullException(nameof(user.Password));
            }

            string salt = await _hashService.ComputeSalt();
            string hashedPassword = await _hashService.ComputeHash(salt, user.Password);

            user.Password = hashedPassword;
            Console.WriteLine(user.Password);
            Console.WriteLine(salt);
            user.Salt = salt;
        }

        // Compare User Password with Salt 
        public async Task<bool> ValidatePassword(User user, string password)
        {
            string salt = user.Salt;

            string hashedPassword = await _hashService.ComputeHash(salt, password);
            Console.WriteLine(user.Password == hashedPassword);
            return user.Password == hashedPassword;
        }

    }
}
