using System.Text.RegularExpressions;

namespace Api.Validators;

/// <summary>
/// Класс констант валидации
/// </summary>
public static class CommonValidatorContants
{
    /// <summary>
    /// Регулярное выражения для проверки пароля
    /// </summary>
    public static readonly Regex ValidatePasswordRegex = new Regex("(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])");

    /// <summary>
    /// Минимальная длинна пароля
    /// </summary>
    public static readonly int PasswordMinLength = 6;

    /// <summary>
    /// Максимальная длинна пароля
    /// </summary>
    public static readonly int PasswordMaxLength = 32;
}