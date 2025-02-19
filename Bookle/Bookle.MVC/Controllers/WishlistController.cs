using Bookle.BL.ViewModels.WishlistVMs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

public class WishlistController : Controller
{
	[HttpGet]
	public IActionResult AddToWishlist(int id, string title)
	{
		var wishlist = GetWishlistFromCookies();

		if (wishlist.FirstOrDefault(x => x.Id == id) == null)
		{
			wishlist.Add(new WishlistCookieItemVM { Id = id, Title = title });
		}

		string data = JsonSerializer.Serialize(wishlist);
		HttpContext.Response.Cookies.Append("wishlist", data, new CookieOptions
		{
			Expires = DateTime.UtcNow.AddDays(30),  
			HttpOnly = true
		});

		return Ok(new { message = "Məhsul wishlist-ə elave edildi!" });
	}

	private List<WishlistCookieItemVM> GetWishlistFromCookies()
	{
		try
		{
			int userId = GetUserId();
			string cookieKey = userId > 0 ? $"wishlist_{userId}" : "wishlist"; 

			string? value = HttpContext.Request.Cookies[cookieKey];

			return string.IsNullOrEmpty(value)
				? new List<WishlistCookieItemVM>()
				: JsonSerializer.Deserialize<List<WishlistCookieItemVM>>(value) ?? new List<WishlistCookieItemVM>();
		}
		catch (Exception)
		{
			return new List<WishlistCookieItemVM>();
		}
	}
	private int GetUserId()
	{
		return int.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId) ? userId : 0;
	}
	[HttpGet]
	public IActionResult GetWishlist()
	{
		var wishlist = GetWishlistFromCookies(); 
		return View(wishlist); 
	}

}
