using Domain.Models;
using MediatR;

namespace Application.Posts.Commands
{
	public class DeletePost : IRequest // in Delete we do not return anything, check PostRepository
	{
		public int postId { get; set; }
	}
}
