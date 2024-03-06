using Application.Abstraction;
using Application.Posts.CommandHandlers;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalApi.Extensions
{
	public static class MinimalApiExtensions
	{
		public static void RegisterServices(this WebApplicationBuilder builder)
		{
			var cs = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<SocialDbContext>(option => option.UseSqlServer(cs));
			builder.Services.AddScoped<IPostsRepository, PostRepository>();
			builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
		}

		public static void RegisterEndpointDefinitions(this WebApplication app)
		{
			//typeof operator in C# allows you to retrieve the type information for any type at runtime.
			//typeof(Program), you're obtaining the type information for the Program class
			//An assembly is the fundamental unit of deployment and versioning in .NET applications. It's a self-contained package that contains compiled code (types like Program), resources (images, data files), and metadata (information about the contents).
			//Each type in .NET belongs to a specific assembly. The .Assembly property on a type object (like typeof(Program)) gives you a reference to the assembly that contains the type definition.
			// so typeof(Program).Assembly give us the assembly of the type of the program.cs
			var endpointDefinitions = typeof(Program).Assembly
				.GetTypes() //get the all types
				.Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) // type have to implement IEndpointDefinition (in this case only PostEndpointDefinition implement IEndpointDefinition)
					&& !t.IsAbstract && !t.IsInterface) // the type have to not be abstract and interface,
				.Select(Activator.CreateInstance) //.Select(...) applies a projection onto the filtered types. It transforms each element of the sequence (the types) into a new form using a specified projection method.   Activator.CreateInstance: This is a static method in the Activator class that allows you to create an instance of a type dynamically at runtime. so we make instance to those filtred type, in this case we creat instance for PostEndpointDefinition
				.Cast<IEndpointDefinition>(); // now we have thoses types who is in our case PostEndpointDefinition, this will casts (converts) the sequence of objects into a collection of IEndpointDefinition interfaces, so Iterates through each object in the sequence.
											  //Checks if it can be cast to the IEndpointDefinition interface (which is guaranteed due to the filtering).
											  //Creates a new reference to the object, treating it as type IEndpointDefinition.
											  //Returns a new collection of these interface references.

			//The endpointDefinitions variable now holds a collection (IEnumerable<IEndpointDefinition>?) of IEndpointDefinition objects (in this case only PostEndpointDefinition), ready to be used in ways that rely on the interface's contract.
			//so he can implement RegisterEndpoints methode who is defined in IEndpointDefinition interface
			foreach (var endpointDef in endpointDefinitions)
			{
				endpointDef.RegisterEndpoints(app);
			}

			//we do that so we can add our endpoints to program.cs once even if we have more than one, in our case we have only one who is PostEndpointDefinition, but in real situation we have a bunch of this endpoints and it is not best practice to add them one by one
		}
	}
}
