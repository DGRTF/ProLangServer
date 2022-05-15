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
    bool CheckCurrentToken(Guid sessionId);

    /// <summary>
    /// Добавляет токен в хранилище
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="expireTimestamp">Тамстам с начала эпохи Unix</param>
    void AddToken(Guid sessionId);

    /// <summary>
    /// Удаляет токены с истекшим временем жизни из хранилища
    /// </summary>
    void ClearToken(Guid sessionId);
}