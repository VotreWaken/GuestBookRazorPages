namespace GuestBookRazorPages.Services
{
    public interface IHash
    {
        // Compute Hash Method
        Task<string> ComputeHash(string salt, string data);

        // Compute Salt Method
        Task<string> ComputeSalt();
    }
}
