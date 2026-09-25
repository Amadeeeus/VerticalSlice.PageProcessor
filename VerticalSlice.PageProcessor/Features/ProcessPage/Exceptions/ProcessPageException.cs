namespace VerticalSlice.PageProcessor.Features.ProcessPage.Exceptions;

public sealed class ProcessPageException(string errorCode, string message, Exception? innerException)
    : Exception(message, innerException)
{
    public string ErrorCode { get; } = errorCode;
}