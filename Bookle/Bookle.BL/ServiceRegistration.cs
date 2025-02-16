using Bookle.BL.Services.Implements;
using Bookle.BL.Services.Interfaces;
using Bookle.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Bookle.BL
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServices(this IServiceCollection services) 
		{
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IRatingService, RatingService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped<IUserService, UserService>();

			return services;	
		}
	}
}
