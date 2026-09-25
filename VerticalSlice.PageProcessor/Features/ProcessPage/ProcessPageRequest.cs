using System.Text.Json.Serialization;

namespace VerticalSlice.PageProcessor.Features.ProcessPage;

/// <summary>
/// Входной DTO 
/// </summary>
public sealed record ProcessPageRequest
{
    /// <summary>
    /// Селектор
    /// </summary>
    [JsonPropertyName("selector")]
    public string? Selector {get; init; }
    
    /// <summary>
    /// HTML атрибут
    /// </summary>
    [JsonPropertyName("attribute")]
    public string? Attribute {get; init; }

    /// <summary>
    /// URL адрес, закодированный в Base64. Дополнительно зашифрован в AES-256
    /// </summary>
    [JsonPropertyName("url_b64")]
    public string? Url {get; init; }
    
    /// <summary>
    /// Текст, закодированный в Base64. Дополнительно зашифрован в AES-256
    /// </summary>
    [JsonPropertyName("encrypted_text_bytes_b64")]
    public string? EncryptedTextBytes {get; init; }
    
    /// <summary>
    /// ключ AES-256, закодированный в Base64
    /// </summary>
    [JsonPropertyName("key_bytes_b64")]
    public string? KeyBytes {get; init; }
    
    /// <summary>
    /// Текст, закодированный в Base64
    /// </summary>
    [JsonPropertyName("page_b64")]
    public string? Page {get; init; }
}