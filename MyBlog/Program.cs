using Microsoft.AspNetCore.Identity;
using MyBlog.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BlogDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BlogDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    // allow users with EmailConfirmed value 0/false to log in
    options.SignIn.RequireConfirmedAccount = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string[] roleNamesList = new string[] { "User", "Admin" };

    foreach (var roleName in roleNamesList)
    {
        if (!roleManager.RoleExistsAsync(roleName).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleName;
            IdentityResult result = roleManager.CreateAsync(role).Result;
            // WARNING: we ignore any errors that Create may return, they should be AT LEAST logged
            // foreach (var error in result.Errors)
            // TODO: Log it
        }
    }

    string adminEmail = "admin@admin.com";
    string adminPass = "Admin123!";
    if (userManager.FindByNameAsync(adminEmail).Result == null)
    {
        IdentityUser user = new IdentityUser();
        user.UserName = adminEmail;
        user.Email = adminEmail;
        user.EmailConfirmed = true;
        IdentityResult result = userManager.CreateAsync(user, adminPass).Result;

        if (result.Succeeded)
        {
            var result2 = userManager.AddToRoleAsync(user, "Admin").Result;
            // if (!result2.Succeeded)
            // FIXME: log the error
        }
        //FIXME: log the error
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();