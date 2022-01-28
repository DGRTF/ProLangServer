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
        User findUser = await FindUserWithRoleByEmailAsync(model.Email);

        if (findUser.Email == string.Empty)
            return new AuthorizeUserResponse("Пользователь с таким адресом электронной почты не был найден");

        var result = await _userManager.ConfirmEmailAsync(findUser, model.Token);

        if (result.Succeeded)
            return new AuthorizeUserResponse(true, findUser.Role.Name);

        return new AuthorizeUserResponse("Неверная ссылка для подтверждения пароля");
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> Login(LoginUser user)
    {
        var findUser = await FindUserWithRoleByEmailAsync(user.Email);

        if (findUser.Email == string.Empty)
            return new AuthorizeUserResponse("Неправильный логин или пароль");

        if (!findUser.EmailConfirmed)
            return new AuthorizeUserResponse("Вы не подтвердили адрес электронной почты");

        var isValidPassword = await _userManager.CheckPasswordAsync(findUser, user.Password);

        if (!isValidPassword)
            return new AuthorizeUserResponse("Неправильный логин или пароль");

        return new AuthorizeUserResponse(isValidPassword, findUser.Role.Name);
    }

    private async Task<User> FindUserWithRoleByEmailAsync(string email) => await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.NormalizedEmail ==
                 _userManager.NormalizeEmail(email)) ?? new User();

    /// <inheritdoc />
    public async Task<RegisterUserResponse> RegisterUser(RegisterUser model)
    {
        var role = await _roleManager.FindByNameAsync(RoleConstants.UserRole)
            ?? new Role(RoleConstants.UserRole);

        var findUser = await _userManager.FindByEmailAsync(model.Email);

        if (findUser != null)
            return new RegisterUserResponse("Пользователь с таким адресом  электронной почты уже существует");

        User user = new User(role, RoleConstants.DefaultUserName, model.Email);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new RegisterUserResponse("Пользователь не создан");

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return new RegisterUserResponse(result.Succeeded, code);
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> ChangePassword(ChangePassword model)
    {
        var user = await FindUserWithRoleByEmailAsync(model.Email);

        if (user.Email == string.Empty)
            return new AuthorizeUserResponse("Неправильные имя пользовател или пароль");

        var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

        if (!result.Succeeded)
            return new AuthorizeUserResponse("Неправильные имя пользовател или пароль");

        return new AuthorizeUserResponse(true, user.Role.Name);
    }

    /// <inheritdoc />
    public async Task<ResetPasswordResponse> ResetPassword(ConfirmUserEmail model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email) ?? new User();

        if (user.Email == string.Empty)
            new RegisterUserResponse("Пользователь с такой электронной почтой не найден");

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

        if (!result.Succeeded)
            return new ResetPasswordResponse("Неверный токен сброса пароля");

        return new ResetPasswordResponse(true);
    }

    public async Task<RegisterUserResponse> ForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? new User();

        if (user.Email == string.Empty)
            return new RegisterUserResponse("Пользователь с такой электронной почтой не найден");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user) ?? string.Empty;

        return new RegisterUserResponse(true, token);
    }
}