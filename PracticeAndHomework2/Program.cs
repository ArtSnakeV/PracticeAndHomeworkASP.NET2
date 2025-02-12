var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Add("home.html"); // Let's set up home.html as a default file
app.UseDefaultFiles(options); // Making options as a default value
app.UseStaticFiles(); // Added to use static files

//////////////////////////////////////////
// Fibonacci number calculation function
int FibonacciNumber(int a)
{
    if (a > 40 || a < 0) // Returning `-2` if value is out of our bounds
    {
        return -1;
    }
    if (a == 0) // if `0`, number is `0`
        return 0;
    else if (a == 1)
    {
        return 1; // if `1`, number is `1`
    }
    else
    {
        return FibonacciNumber(a - 1) + FibonacciNumber(a - 2);
    }
}

// Function to check result from FibonacciNumber function
string CheckFNumber(int t)
{
    int a = FibonacciNumber(t);
    if (a == -1)
        return "Please check, that Your value is in range: 0<n<40. Thank You very much";
    return a.ToString();
}

/////////////////////////////////////////

app.Run(async (context) =>
{
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
    //////////////////////
    /// Fibonacci
    else if (path.Equals("/fibonacci", StringComparison.OrdinalIgnoreCase)) {
        if (int.TryParse(context.Request.Query["index"], out int indexValue))
        {
            await context.Response.WriteAsync($"Fibonacci number of `{indexValue}` is: {CheckFNumber(indexValue)}");
        }
        else
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Please provide a valid index parameter.");
        }
    }
    ////////////////////////////
    else
    {
        // If no file is found, return a 404 response
        context.Response.StatusCode = 404;

        await context.Response.WriteAsync("404 Not Found");
    }
});

app.Run();


