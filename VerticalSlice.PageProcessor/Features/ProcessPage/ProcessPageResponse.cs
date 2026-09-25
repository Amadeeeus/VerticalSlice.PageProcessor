using System.Text.Json.Serialization;

namespace VerticalSlice.PageProcessor.Features.ProcessPage;

/// <summary>
/// Выходное DTO
/// </summary>
public sealed record ProcessPageResponse
{
    /// <summary>
    /// Признак наличия ошибки
    /// </summary>
    [JsonPropertyName("is_error")]
    public int IsError {get; init;}
    
    /// <summary>
    /// Код ошибки
    /// </summary>
    [JsonPropertyName("error_code")]
    public string? ErrorCode {get; init;}
    
    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    [JsonPropertyName("error_message")]
    public string? ErrorMessage {get; init;}
    
    /// <summary>
    /// Количество элементов, найденных по селектору
    /// </summary>
    [JsonPropertyName("elements_count")]
    public int ElementsCount {get; init;}
    
    /// <summary>
    /// Количество найденных Email адресов
    /// </summary>
    [JsonPropertyName("emails_count")]
    public int EmailsCount {get; init;}
    
    /// <summary>
    /// Адрес после декодирования
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url {get; init;}
    
    /// <summary>
    /// Декодированный текст
    /// </summary>
    [JsonPropertyName("decrypted_plain_text")]
    public string? DecryptedPlainText {get; init;}

    /// <summary>
    /// Список значений атрибутов
    /// </summary>
    [JsonPropertyName("elements_attr_list")] 
    public IReadOnlyList<string> ElementsAttrList { get; init; } = [];
    
    /// <summary>
    /// Список Email адресов
    /// </summary>
    [JsonPropertyName("emails_list")]
    public IReadOnlyList<string> EmailsList { get; init; } = [];
}