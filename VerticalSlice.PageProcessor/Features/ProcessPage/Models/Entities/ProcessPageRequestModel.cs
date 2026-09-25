namespace VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;

public sealed class ProcessPageRequestModel
{
    /// <summary>
    /// Селектор
    /// </summary>
    public required string Selector {get; set; }
    
    /// <summary>
    /// HTML атрибут
    /// </summary>
    public required string Attribute {get; set; }

    /// <summary>
    /// URL адрес, закодированный в Base64. Дополнительно зашифрован в AES-256
    /// </summary>
    public required string Url {get; set; }
    
    /// <summary>
    /// Текст, закодированный в Base64. Дополнительно зашифрован в AES-256
    /// </summary>
    public required string EncryptedTextBytes {get; set; }
    
    /// <summary>
    /// ключ AES-256, закодированный в Base64
    /// </summary>
    public required string KeyBytes {get; set; }
    
    /// <summary>
    /// Текст, закодированный в Base64
    /// </summary>
    public required string Page {get; set; }
}