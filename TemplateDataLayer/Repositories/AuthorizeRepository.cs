using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TemplateDataLayer.Contexts;
using TemplateDataLayer.Models.Authorize;
using UserLogic.ExternalInterfaces;
using UserLogic.Models;

namespace TemplateDataLayer.Repositories;

/// <inheritdoc />
public class AuthorizeRepository : IAuthorizeRepository
{
    private readonly UserManager<User> _userManager;
    private readonly AuthorizeContext _context;
    private readonly RoleManager<Role> _roleManager;

    public AuthorizeRepository(
        RoleManager<Role> roleManager,
         UserManager<User> userManager,
         AuthorizeContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        this._context = context;
        _userManager.UserValidators.Clear();
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> Login(LoginUser user)
    {
        var findUser = await _context.Users
            .Include(x=> x.Role)
            .FirstOrDefaultAsync(x => x.Email == user.Email);

        if (findUser == null)
            return new AuthorizeUserResponse(false, string.Empty);

        var isValidPassword = await _userManager.CheckPasswordAsync(findUser, user.Password);

        return new AuthorizeUserResponse(isValidPassword, findUser?.Role?.Name ?? string.Empty);
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> RegisterUser(RegisterUser model)
    {
        var role = await _roleManager.FindByNameAsync(RoleConstants.UserRole)
            ?? new Role(RoleConstants.UserRole);

        var findUser = await _userManager.FindByEmailAsync(model.Email);

        if (findUser != null)
            return new AuthorizeUserResponse(false, RoleConstants.UserRole);

        User user = new User(role, RoleConstants.DefaultUserName, model.Email);
        var result = await _userManager.CreateAsync(user, model.Password);

        return new AuthorizeUserResponse(result.Succeeded, RoleConstants.UserRole);
    }
}