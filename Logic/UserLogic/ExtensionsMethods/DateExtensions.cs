namespace UserLogic.ExtensionsMethods;

public static class DateExtensions
{
    public static int ToUnixTimeStamp(this DateTime input)
    {
        return (int)input.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
}