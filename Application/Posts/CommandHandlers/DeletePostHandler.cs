using Application.Abstraction;
using Application.Posts.Commands;
using MediatR;

namespace Application.Posts.CommandHandlers
{
	// IRequestHandler interface from MediatR
	public class DeletePostHandler : IRequestHandler<DeletePost>
	{
		private readonly IPostsRepository _postsRepo;
        public DeletePostHandler(IPostsRepository postsRepo)
        {
			_postsRepo = postsRepo;

		}

		public async Task Handle(DeletePost request, CancellationToken cancellationToken) // Unit is a struct that represents a void type, because DeletePost doesn't return anything
		{
			await _postsRepo.DeletePost(request.postId);
		}
	}
}
