using Bookle.Core.Repositories;
using Bookle.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bookle.DAL
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services) 
		{
			services.AddScoped<IBookRepository, BookRepository>();
			services.AddScoped<IAuthorRepository, AuthorRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();
			return services;		
		}
	}
}
