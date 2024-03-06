using Application.Abstraction;
using Application.Posts.Commands;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers
{
	// IRequestHandler interface from MediatR
	public class CreatePostHandler : IRequestHandler<CreatePost, Post> // the first one "CreatePost" is the command for which this handler will handle the request and then Post the return type
	{
		private readonly IPostsRepository _postsRepo;
        public CreatePostHandler(IPostsRepository postsRepo)
        {
			_postsRepo = postsRepo;

		}

        public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
		{
			var newPost = new Post
			{
				Content = request.postContent
			};
			// for DateCreated and LastModified is handle by CreatePost in PostRepository in DataAccess Project, and Id is handle by database
			return await _postsRepo.CreatePost(newPost);
		}
	}
}
