namespace UserLogic.ExternalInterfaces;

/// <summary>
/// Представляет хранение токенов
/// </summary>
public interface ITokensRepository
{
    /// <summary>
    /// Проверяет наличие токена в хранилище
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="expireTimestamp">Тамстам с начала эпохи Unix</param>
    /// <returns>Находится ли токен в хранилище</returns>
    Task<bool> CheckCurrentToken(Guid userId, int expireTimestamp);

    /// <summary>
    /// Добавляет токен в хранилище
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="expireTimestamp">Тамстам с начала эпохи Unix</param>
    Task AddToken(Guid userId, int expireTimestamp);

    /// <summary>
    /// Удаляет токены с истекшим временем жизни из хранилища
    /// </summary>
    Task ClearExpiredTokens();
}