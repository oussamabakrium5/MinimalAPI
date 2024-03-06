using Domain.Models;
using MediatR;

namespace Application.Posts.Commands
{
	public class UpdatePost : IRequest<Post>
	{
		public int postId { get; set; }
		public string? postContent { get; set; }
	}
}
