using System.ComponentModel.DataAnnotations;

namespace Bookle.BL.ViewModels.AuthsVMs
{
 public	class LoginVM
    {
		public string UsernameOrEmail { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
