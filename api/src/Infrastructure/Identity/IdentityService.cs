using System.Security.Claims;

using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        ApplicationUser? user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<bool> LoginUserAsync(string userName, string password, bool isPersistent)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, true);
        return result.Succeeded;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        ApplicationUser user = new() {UserName = userName, Email = userName};

        IdentityResult? result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
        }

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        ClaimsPrincipal? principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        AuthorizationResult result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        IdentityResult? result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}