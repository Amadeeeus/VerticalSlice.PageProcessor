using VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;

namespace VerticalSlice.PageProcessor.Shared.Constants;

/// <summary>
/// Константы для команд SQL.
/// </summary>
public class DatabaseConstants
{
    public const string ADD_ELEMENT = "INSERT INTO elements (attribute_value, html) values (@AttributeValue, @Html)";
}