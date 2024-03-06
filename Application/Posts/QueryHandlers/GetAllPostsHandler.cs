using Application.Abstraction;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;

namespace Application.Posts.QueryHandlers
{
	public class GetAllPostsHandler : IRequestHandler<GetAllPosts, ICollection<Post>>
	{
		private readonly IPostsRepository _postsRepo;
		public GetAllPostsHandler(IPostsRepository postsRepo)
		{
			_postsRepo = postsRepo;

		}

		public async Task<ICollection<Post>> Handle(GetAllPosts request, CancellationToken cancellationToken)
		{
			return await _postsRepo.GetAllPosts();
		}
	}
}
