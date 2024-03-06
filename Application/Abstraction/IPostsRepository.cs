using Domain.Models;

namespace Application.Abstraction
{
	public interface IPostsRepository
	{
		Task<ICollection<Post>> GetAllPosts();
		Task<Post> GetPostById(int postId);
		Task<Post> CreatePost(Post toCreate);
		Task<Post> UpdatePost(string updatedContent, int postId);
		Task DeletePost(int postId);
	}
}
