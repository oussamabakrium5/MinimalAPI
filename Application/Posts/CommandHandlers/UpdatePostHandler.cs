using Application.Abstraction;
using Application.Posts.Commands;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers
{
	public class UpdatePostHandler : IRequestHandler<UpdatePost, Post>
	{
		private readonly IPostsRepository _postsRepo;
		public UpdatePostHandler(IPostsRepository postsRepo)
		{
			_postsRepo = postsRepo;

		}
		public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
		{
			var post = await _postsRepo.UpdatePost(request.postContent, request.postId);
			return post;
		}
	}
}
