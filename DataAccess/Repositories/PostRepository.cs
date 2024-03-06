using Application.Abstraction;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
	public class PostRepository : IPostsRepository
	{
		private readonly SocialDbContext _context;
        public PostRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(Post toCreate)
		{
			toCreate.DateCreated = DateTime.Now;
			toCreate.LastModified = DateTime.Now;
			_context.Posts.Add(toCreate);
			await _context.SaveChangesAsync();
			return toCreate;
		}

		public async Task DeletePost(int postId)
		{
			var post = await _context.Posts
				.FirstOrDefaultAsync(p => p.Id == postId);
			if (post == null)
			{
				return;
			}
			_context.Posts.Remove(post);
			await _context.SaveChangesAsync();
		}

		public async  Task<ICollection<Post>> GetAllPosts()
		{

			//we can also define interface that return IEnumerable instead of ICollection and just return await _context.Posts
			//difference that when we use ToListAsync() it load into memory entierly, when we use IEnumerable or IQuerable, so it stream the data not loading everything into memory, and that is important for optimizing queries
			return await _context.Posts.ToListAsync();
		}

		public async Task<Post> GetPostById(int postId)
		{
			return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
		}

		public async Task<Post> UpdatePost(string updatedContent, int postId)
		{
			var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
			post.LastModified = DateTime.Now;
			post.Content = updatedContent;
			await _context.SaveChangesAsync();
			return post;
		}
	}
}
