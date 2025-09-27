using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Models;
using TareasMVC.Services;

namespace TareasMVC.Controllers;

public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _dbContext;

    [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
    public UsersController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
    }

    [AllowAnonymous]
    public IActionResult Register() => View();

    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new IdentityUser { Email = model.Email, UserName = model.Email };

        var registerResult = await _userManager.CreateAsync(user, password: model.Password);

        if (!registerResult.Succeeded)
        {
            foreach (var error in registerResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        await _signInManager.SignInAsync(user, isPersistent: true);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var signinResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (signinResult.Succeeded)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError(string.Empty, "Incorrect password or email");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    [Authorize(Roles = Constants.RolAdmin)]
    public async Task<IActionResult> UserList(string? message = null)
    {
        var users = await _dbContext.Users
            .Select(u => new UserViewModel
            {
                Email = u.Email
            })
            .ToListAsync();

        return View(new UserListViewModel
        {
            Users = users,
            Message = message
        });
    }

    [HttpPost]
    [Authorize(Roles = Constants.RolAdmin)]
    public async Task<IActionResult> DoAdmin(string email)
    {
        var user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        if (user is null)
            return NotFound();

        await _userManager.AddToRoleAsync(user, Constants.RolAdmin);

        return RedirectToAction("UserList", routeValues: new { message = $"Granted {Constants.RolAdmin} rol correctly to {user.Email}" });
    }

    [HttpPost]
    [Authorize(Roles = Constants.RolAdmin)]
    public async Task<IActionResult> RevokeAdmin(string email)
    {
        var user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        if (user is null)
            return NotFound();

        await _userManager.RemoveFromRoleAsync(user, Constants.RolAdmin);

        return RedirectToAction("UserList", routeValues: new { message = $"Revoked {Constants.RolAdmin} rol correctly to {user.Email}" });
    }
}