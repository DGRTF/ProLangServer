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
    public async Task<AuthorizeUserResponse> ConfirmEmail(ConfirmUserEmail model)
    {
        User findUser = await FindUserByEmail(model.Email);

        if (findUser.Email == string.Empty)
            return GetUnsuccessfulAuthorizeResponse();

        var result = await _userManager.ConfirmEmailAsync(findUser, model.Token);

        if (result.Succeeded)
            return new AuthorizeUserResponse(true, findUser.Role.Name);

        return GetUnsuccessfulAuthorizeResponse();
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> Login(LoginUser user)
    {
        var findUser = await FindUserByEmail(user.Email);

        if (!findUser.EmailConfirmed)
            return GetUnsuccessfulAuthorizeResponse();

        var isValidPassword = await _userManager.CheckPasswordAsync(findUser, user.Password);

        return new AuthorizeUserResponse(isValidPassword, findUser.Role.Name);
    }

    private async Task<User> FindUserByEmail(string email) => await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.NormalizedEmail  ==
                 _userManager.NormalizeEmail(email)) ?? new User();

    private AuthorizeUserResponse GetUnsuccessfulAuthorizeResponse() => new AuthorizeUserResponse(false, string.Empty);

    /// <inheritdoc />
    public async Task<RegisterUserResponse> RegisterUser(RegisterUser model)
    {
        var role = await _roleManager.FindByNameAsync(RoleConstants.UserRole)
            ?? new Role(RoleConstants.UserRole);

        var findUser = await _userManager.FindByEmailAsync(model.Email);

        if (findUser != null)
            return GetUnsuccessfulRegisterResponse();

        User user = new User(role, RoleConstants.DefaultUserName, model.Email);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return GetUnsuccessfulRegisterResponse();

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return new RegisterUserResponse(result.Succeeded, code);
    }

    private RegisterUserResponse GetUnsuccessfulRegisterResponse() => new RegisterUserResponse(false, string.Empty);
}