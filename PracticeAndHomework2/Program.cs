var builder = WebApplication.CreateBuilder(args); var app = builder.Build();

DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Add("home.html"); // Let's set up home.html as a default file
app.UseDefaultFiles(options); // Making options as a default value
app.UseStaticFiles(); // Added to use static files

app.Run(async (context) => {
    context.Response.Headers["Content-Type"] = "text/html; charset = utf-8";

    string path = context.Request.Path;
    

    // Check for specific routes and serve files
    if (path == "/home")
    {
        await context.Response.SendFileAsync("wwwroot/home.html");
    }
    else if (path.Equals("/biography", StringComparison.OrdinalIgnoreCase))
    {
        await context.Response.SendFileAsync("wwwroot/Biography.html");
    }
    else if (path.Equals("/portfolio", StringComparison.OrdinalIgnoreCase))
    {
        await context.Response.SendFileAsync("wwwroot/Portfolio.html");
    }
    else
    {
        // If no file is found, return a 404 response
        context.Response.StatusCode = 404;

        await context.Response.WriteAsync("404 Not Found");
    }
});

app.Run();


