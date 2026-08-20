var builder = WebApplication.CreateBuilder(args);

// Add MVC service
builder.Services.AddControllersWithViews();

// Add session service
builder.Services.AddSession();

// Add authorization service
builder.Services.AddAuthorization();

var app = builder.Build();

// Show error page in production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Use CSS, JavaScript and images
app.UseStaticFiles();

app.UseRouting();

// Use session before authorization
app.UseSession();

app.UseAuthorization();

// Open registration page first
app.MapGet("/", () =>
{
    return Results.Redirect("/Student/Register");
});

// Use routes written in controllers
app.MapControllers();

// Use default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();