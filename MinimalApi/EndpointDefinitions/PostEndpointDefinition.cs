using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Abstractions;
using MinimalApi.Filters;

namespace MinimalApi.EndpointDefinitions
{
	public class PostEndpointDefinition : IEndpointDefinition
	{
		public void RegisterEndpoints(WebApplication app)
		{
			var posts = app.MapGroup("/api/posts");

			// .Net 8 will make sure that he inject in GetPostById the id and mediator
			//.WithName("GetPostById"); is a metadata information
			posts.MapGet("/{id}", GetPostById).
				WithName("GetPostById");
			posts.MapPost("/", CreatePost)
				.AddEndpointFilter<PostValidationFilter>();
			posts.MapGet("/", GetAllPosts);
			posts.MapPut("/{id}", UpdatePost)
				.AddEndpointFilter<PostValidationFilter>();
			posts.MapDelete("/{id}", DeletePost);
		}

		private async Task<IResult> GetPostById(IMediator mediator, int id)
		{
			var getPost = new GetPostById { PostId = id }; // GetPostById from Application Layer
			var post = await mediator.Send(getPost);
			return TypedResults.Ok(post);
			// we can do TypedResults and Results, but TypedResults better because it return 200 or 404 etc and that make testing minimal api easier
		}

		private async Task<IResult> CreatePost(IMediator mediator, [FromBody] Post post)
		{
			var createPost = new CreatePost { postContent = post.Content };
			var createdPost = await mediator.Send(createPost);
			return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
		}
		//(IMediator mediator, Post post) will look automatically in the body to find data with Post type, so [FromBody] it is not mandatory
		//	return Results.CreatedAtRoute("GetPostById", new {createdPost.Id}, createdPost);  so after this post will go to find a endpoint .WithName("GetPostById");
		//	and will give the createdPost.Id in /api/posts/{id} and createdPost in the body

		private async Task<IResult> GetAllPosts(IMediator mediator)
		{
			var getCommand = new GetAllPosts();
			var posts = await mediator.Send(getCommand);
			return TypedResults.Ok(posts);
		}

		private async Task<IResult> UpdatePost(IMediator mediator, [FromBody] Post post, int id)
		{
			var updatePost = new UpdatePost { postId = id, postContent = post.Content };
			var updatedPost = await mediator.Send(updatePost);
			return TypedResults.Ok(updatedPost);
		}

		private async Task<IResult> DeletePost(IMediator mediator, int id)
		{
			var deletePost = new DeletePost { postId = id };
			await mediator.Send(deletePost);
			return TypedResults.NoContent();
		}
	}
}
