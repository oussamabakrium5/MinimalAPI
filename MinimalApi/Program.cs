using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
var app = builder.Build();

// Configure the HTTP request pipeline.

// we do exception in middleware because exception filters does not existe in minimal API
//Global exception handling middleware
app.Use(async(context, next) =>
{
	try
	{
		await next();
		//means that if anywhere down the line it will throw an err or an exception, we will catch it here (in catch (Exception) )
	}
	catch (Exception)
	{
		context.Response.StatusCode = 500;
		await context.Response.WriteAsync("An error ocurred");
	}
});
app.UseHttpsRedirection();

app.RegisterEndpointDefinitions();

app.Run();