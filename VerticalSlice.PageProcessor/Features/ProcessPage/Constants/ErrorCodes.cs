namespace VerticalSlice.PageProcessor.Features.ProcessPage.Constants;

public static class ErrorCodes
{
    public const string InvalidUrlBase64 = "INVALID_URL_BASE64";
    public const string InvalidPageBase64 = "INVALID_PAGE_BASE64";
    public const string InvalidEncryptedTextBase64 = "INVALID_ENCRYPTED_TEXT_BASE64";
    public const string InvalidKeyBase64 = "INVALID_KEY_BASE64";
    public const string DecryptionError = "DECRYPTION_ERROR";
    public const string InternalError = "INTERNAL_ERROR";
    public const string ValidationError = "VALIDATION_ERROR";
}