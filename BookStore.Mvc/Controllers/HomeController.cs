using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;
using BookStore.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Mvc.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private string sessionId = "fdsjggefdsij53498rjdsgimsifee3jnfdsnjsam4egjr89tj4gjvdfsavcdsaf43fr4tqw";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // var currentUser = new User
        // {
        //     Username = "User@mail.com",
        //     Password = "",
        //     SessionId = sessionId,
        //     Role = EnumRole.Admin,
        //     Authenticated = false
        // };
        var sessionCookie = Request.Cookies["session_id"];
        var ownerId = GetOwnerId(sessionCookie);
        // if (sessionCookie == currentUser.SessionId)
        // {
        //     currentUser.Authenticated = true;
        // }
        var viewModel = new { OwnerId = ownerId };
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (User.Role != EnumRole.Admin)
            return Unauthorized();
        
        var correctPassword = "test";


        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Please provide both username and password");
            return View(model);
        }

        // TODO: Add your login logic here
        if (model.Password != correctPassword)
        {
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        Response.Cookies.Append("session_id", sessionId, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddDays(7)
        });

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}

public class LoginViewModel
{
    [Required] public string? Username { get; set; }

    [Required] public string? Password { get; set; }
}

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public EnumRole Role { get; set; }
    public string SessionId { get; set; }

    [IgnoreDataMember] public bool Authenticated { get; set; }
}

public enum EnumRole
{
    Admin,
    User
}
