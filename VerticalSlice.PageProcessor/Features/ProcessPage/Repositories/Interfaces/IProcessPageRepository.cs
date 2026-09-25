using VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;

namespace VerticalSlice.PageProcessor.Features.ProcessPage.Repositories.Interfaces;

public interface IProcessPageRepository
{
    Task SaveElementAsync(IReadOnlyCollection<ElementEntity> element, CancellationToken ct);
}