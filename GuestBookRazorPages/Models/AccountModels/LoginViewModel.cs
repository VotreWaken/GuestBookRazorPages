using System.ComponentModel.DataAnnotations;

namespace GuestBookRazorPages.Models.AccountModels
{
	public class LoginViewModel
	{
		[Required]
		public string? Login { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
