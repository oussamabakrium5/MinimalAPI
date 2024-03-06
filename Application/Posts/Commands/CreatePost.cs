using Domain.Models;
using MediatR;

namespace Application.Posts.Commands
{
	// IRequest interface from MediatR
	public class CreatePost : IRequest<Post> // Post is the return type
	{
		public string? postContent { get; set; }
	}
}
