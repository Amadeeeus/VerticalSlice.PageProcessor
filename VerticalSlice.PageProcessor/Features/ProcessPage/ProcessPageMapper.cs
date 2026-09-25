using AutoMapper;
using VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;

namespace VerticalSlice.PageProcessor.Features.ProcessPage;

/// <summary>
/// Маппер для преобразования входной DTO во внутреннюю модель. Можно красивее реализовать через метод с параметром DTO через this
/// </summary>
public class ProcessPageMapper : Profile
{
    public ProcessPageMapper()
    {
        CreateMap<ProcessPageRequest, ProcessPageRequestModel>();
    }
}