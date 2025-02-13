using System.Data;
using Bookle.Core.Enums;
namespace Bookle.BL.Extentions;

	public static class RoleExtention
{
	public static string GetRole(this Role role)
	{
		return role switch
		{
			Role.Admin => nameof(Role.Admin),
			Role.User => nameof(Role.User),
			Role.Moderator => nameof(Role.Moderator),
			Role.Author => nameof(Role.Author)

		};
	}
}
