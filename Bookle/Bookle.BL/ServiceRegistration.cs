using Bookle.BL.Services.Implements;
using Bookle.BL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bookle.BL
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServices(this IServiceCollection services) 
		{
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IAuthorService, AuthorService>();

			return services;	
		}
	}
}
