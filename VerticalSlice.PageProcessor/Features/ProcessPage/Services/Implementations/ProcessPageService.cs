using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AngleSharp;
using VerticalSlice.PageProcessor.Features.ProcessPage.Constants;
using VerticalSlice.PageProcessor.Features.ProcessPage.Exceptions;
using VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;
using VerticalSlice.PageProcessor.Features.ProcessPage.Repositories.Interfaces;
using VerticalSlice.PageProcessor.Features.ProcessPage.Services.Interfaces;

namespace VerticalSlice.PageProcessor.Features.ProcessPage.Services.Implementations;

/// <summary>
/// Сервис для записи в БД и возврата результата
/// </summary>
public class ProcessPageService(IProcessPageRepository repository) : IProcessPageService
{
    private static readonly Regex EmailRegex = new(@" [A-Za-z0-9._%=-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", 
        RegexOptions.Compiled  |
        RegexOptions.CultureInvariant |
        RegexOptions.IgnoreCase);
    
    /// <summary>
    /// Метод для записи в бд и возврата ответа определенного формата
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns> Сущность для возврата ответа </returns>
    public async Task<ProcessPageResponse> CreateRecordAsync(ProcessPageRequestModel request, CancellationToken ct)
    {
        var url = DecodeBase64(request.Url, "url");
        var page = DecodeBase64(request.Page,  "page");
        
        var context = BrowsingContext.New(Configuration.Default);
        
        var document = await context.OpenAsync(
            req => req.Content(page),
            cancel: ct);

        var elements = document.QuerySelectorAll(request.Selector);

        var attributeValues = elements
            .Select(x => x.GetAttribute(request.Attribute) ?? string.Empty)
            .ToList();

        var entities = elements.Select(x => new ElementEntity
        {
            AttributeValue = x.GetAttribute(request.Attribute) ?? string.Empty,
            Html = x.OuterHtml
        })
            .ToList();
        
        var emails = EmailRegex
            .Matches(page)
            .Select(x => x.Value)
            .ToList();

        var decryptedText = Decrypt(request.EncryptedTextBytes, request.KeyBytes);
        
        await repository.SaveElementAsync(entities, ct);
        
        return new  ProcessPageResponse
        {
            IsError =  0,
            Url = url,
            ElementsCount = elements.Length,
            ElementsAttrList =  attributeValues,
            EmailsCount = emails.Count,
            EmailsList =  emails,
            DecryptedPlainText =  decryptedText
        };
    }

    /// <summary>
    /// Декодирование с base64
    /// </summary>
    /// <param name="base64">Строка для декодирования</param>
    /// <param name="fieldName">Имя декодируемого поля</param>
    /// <returns>Декодированная строка</returns>
    private static string DecodeBase64(string base64, string fieldName)
    {
        try
        {
            var bytes = Convert.FromBase64String(base64);
        
            return Encoding.UTF8.GetString(bytes);
        }
        catch (FormatException ex)
        {
            var errorCodes = fieldName == "url" ?
                ErrorCodes.InvalidUrlBase64 :
                ErrorCodes.InvalidPageBase64;
            
            throw new ProcessPageException(errorCodes, $"Некорректное значение поля {fieldName}", ex);

        }
    }

    /// <summary>
    /// Метод расшифровки из AES-256
    /// </summary>
    /// <param name="encryptedText">Текст для расшифровки</param>
    /// <param name="key">Ключ для расшифровки</param>
    /// <returns>Расшифрованная строка</returns>
    private static string Decrypt(string encryptedText, string key)
    {
        byte[] encryptedBytes;
        byte[] keyBytes;
        try
        {
            encryptedBytes = Convert.FromBase64String(encryptedText);
        }
        catch (FormatException ex)
        {
            throw new ProcessPageException(ErrorCodes.InvalidEncryptedTextBase64, "Некорректное значение encrypted_text_bytes_b64", ex);
        }

        try
        {
            keyBytes = Convert.FromBase64String(key);
        }
        catch (FormatException ex)
        {
            throw new ProcessPageException(ErrorCodes.InvalidKeyBase64, "Некорректное значение key_bytes_b64", ex);
        }

        try
        {
            using var aes = Aes.Create();

            aes.KeySize = 256;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;
            aes.Key = keyBytes;

            using var decryptor = aes.CreateDecryptor();
        
            var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0 , encryptedBytes.Length);
        
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        catch (CryptographicException ex)
        {
            throw new ProcessPageException(ErrorCodes.DecryptionError, "Ошибка при расшифровке текста", ex);
        }

    }
}