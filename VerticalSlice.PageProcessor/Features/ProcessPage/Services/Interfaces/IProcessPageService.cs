using VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;

namespace VerticalSlice.PageProcessor.Features.ProcessPage.Services.Interfaces;

/// <summary>
/// Контракт для DI
/// </summary>
public interface IProcessPageService
{
    Task<ProcessPageResponse> CreateRecordAsync(ProcessPageRequestModel request, CancellationToken ct);
}