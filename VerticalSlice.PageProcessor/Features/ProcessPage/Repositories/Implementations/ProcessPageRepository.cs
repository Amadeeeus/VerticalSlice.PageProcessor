using System.Data;
using Dapper;
using Npgsql;
using VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;
using VerticalSlice.PageProcessor.Features.ProcessPage.Repositories.Interfaces;
using VerticalSlice.PageProcessor.Shared.Constants;

namespace VerticalSlice.PageProcessor.Features.ProcessPage.Repositories.Implementations;

/// <summary>
/// Репозиторий для работы с БД через Dapper
/// </summary>
public class ProcessPageRepository(NpgsqlDataSource dataSource) : IProcessPageRepository
{
    public async Task SaveElementAsync(IReadOnlyCollection<ElementEntity> element, CancellationToken ct)
    {
        await using var connection = await dataSource.OpenConnectionAsync(ct);
        
        await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);

        try
        {
            var command = new CommandDefinition(
                DatabaseConstants.ADD_ELEMENT,
                element,
                transaction,
                cancellationToken: ct);

            await connection.ExecuteAsync(command);

            await transaction.CommitAsync(ct);
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}