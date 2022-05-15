using UserLogic.ExternalInterfaces;

namespace TemplateDataLayer.Repositories;

public class TokensRepository : ITokensRepository
{
    private static object _lock = new();
    private static HashSet<Guid> _sessionRepository = new();

    public void AddToken(Guid sessionId)
    {
        lock (_lock)
            _sessionRepository.Add(sessionId);
    }

    public bool CheckCurrentToken(Guid sessionId)
    {
        lock (_lock)
            return _sessionRepository.Contains(sessionId);
    }

    public void ClearToken(Guid sessionId)
    {
        lock (_lock)
            _sessionRepository.Remove(sessionId);
    }
}