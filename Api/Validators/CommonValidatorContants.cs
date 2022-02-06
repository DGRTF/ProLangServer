using System.Text.RegularExpressions;

namespace Api.Validators;

public class CommonValidatorContants
{
    public static readonly Regex ValidatePasswordRegex = new Regex("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{7,15}$");
}