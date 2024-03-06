using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApi.Filters
{
	public class PostValidationFilter : IEndpointFilter
	{
		public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
			EndpointFilterDelegate next)
		{
			var post = context.GetArgument<Post>(1); // if you look at PostEndpointDefinition you will find that if we define an endpoint that needs the types Post, we always put it in the second field, so we can do validation very easy because we can pass the index of 1, the first argument will be in 0 index and the second will be in the 1 argument
													 //so he will look to the http message "context" and get the Post type and give it to post variable to pass it as second parameter in endpoint who use this validation
													 //context: An EndpointFilterInvocationContext object that provides information about the current endpoint invocation.
													 //next: An EndpointFilterDelegate delegate that represents the next step in the endpoint pipeline (typically the actual endpoint handler).
			/*if (string.IsNullOrEmpty(post.Content))
			{
				return await Task.FromResult(Results.BadRequest("Post not valide"));
			}
			.NET 7 (C# 9 and below) have to do that because post can be null so post.Content.IsNullOrEmpty() won't work
			 however in .NET 8 (C# 10) post.Content.IsNullOrEmpty() work fine even if post can be null
			 */

			if (post.Content.IsNullOrEmpty())
			{
				return await Task.FromResult(Results.BadRequest("Post not valide"));
			}
			return await next(context);
		}
	}
}
