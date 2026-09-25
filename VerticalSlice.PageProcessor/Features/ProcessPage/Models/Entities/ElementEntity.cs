namespace VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;

/// <summary>
/// Сущность записи в БД
/// </summary>
public sealed class ElementEntity
{
    /// <summary>
    /// Id в базе данных
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Значение атрибута
    /// </summary>
    public string AttributeValue { get; set; } = string.Empty;
    
    /// <summary>
    /// Полный код найденного элемента
    /// </summary>
    public string Html { get; set; } = string.Empty;
}