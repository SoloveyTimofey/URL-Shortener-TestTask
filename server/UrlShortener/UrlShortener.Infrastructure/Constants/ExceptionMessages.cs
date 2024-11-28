namespace UrlShortener.Infrastructure.Constants;

public static class ExceptionMessages
{
    #region Identity
    public const string UserNameIsAlreadyTaken = "User name is already taken.";
    public const string JwtIsNotSetInTheConfiguration = "Jwt is not set in the configuration.";
    public const string ExpirationTimeIsNotInt = "Jwt Expiration time set in the configuration, but its value is not int.";
    public const string ExpirationTimeIsNotSetInTheConfiguration = "Expiration time is not set in the configuration.";
    public const string InvalidUserNameOrPassword = "Invalid user name or password.";
    #endregion

    #region ShortenedUrl
    public const string ShortenedUrlNotFoundTemplate = "ShortenedUrl with ID {0} not found.";
    public static string ShortenedUrlNotFound(uint id)
    {
        return string.Format(ShortenedUrlNotFoundTemplate, id);
    }

    public const string OriginalURLCannotBeNullOrEmpty = "Original URL cannot be null or empty.";
    public const string FullUrlWithSpecifiedShortenedVersionNotFound = "Full url with specified shortened version not found.";
    #endregion

    public static IEnumerable<string> GetAllMessages()
    {
        yield return UserNameIsAlreadyTaken;
        yield return JwtIsNotSetInTheConfiguration;
        yield return ExpirationTimeIsNotInt;
        yield return ExpirationTimeIsNotSetInTheConfiguration;

        yield return OriginalURLCannotBeNullOrEmpty;
    }
}