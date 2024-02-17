using GuestBookRazorPages.Models.AccountModels;

namespace GuestBookRazorPages.Repository
{
    public interface IAccountRepository : IRepository<User>
    {
        // Get User By Login
        Task<User> GetByLogin(string login);

        // Validate Password
        Task<bool> ValidatePassword(User user, string password);

        // Hash User Password
        Task HashUserPassword(User user);
    }
}
