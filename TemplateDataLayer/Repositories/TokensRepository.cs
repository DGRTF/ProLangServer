using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TemplateDataLayer.Contexts;
using TemplateDataLayer.Models.Authorize;
using UserLogic.ExtensionsMethods;
using UserLogic.ExternalInterfaces;

namespace TemplateDataLayer.Repositories;

public class TokensRepository : ITokensRepository
{
    private readonly UserManager<User> _userManager;
    private readonly AuthorizeContext _context;

    public TokensRepository(UserManager<User> userManager, AuthorizeContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task AddToken(Guid userId, int expireTimestamp)
    {
        var token = new IdentityUserToken<Guid>{
            UserId = userId,
            Value = string.Empty,
            LoginProvider = string.Empty,
            Name = expireTimestamp.ToString().Normalize(),
        };

        await _context.UserTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckCurrentToken(Guid userId, int expireTimestamp)
    {
        return await _context.UserTokens.AnyAsync(x=> x.UserId == userId && x.Name == expireTimestamp.ToString().Normalize());
    }

    public async Task ClearExpiredTokens()
    {
        var removedTokens = _context.UserTokens.Where(x=> x.Name == DateTime.Now.ToUnixTimeStamp().ToString().Normalize());
        _context.RemoveRange(removedTokens);
        await _context.SaveChangesAsync();

        //if(!result.Succeeded)
            // прологировать, что токен не найден, без этого работать можно, но есть явные ошибки в по

            
    }
}