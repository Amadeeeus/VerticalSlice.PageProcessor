using FastEndpoints;
using VerticalSlice.PageProcessor.Features.ProcessPage.Models.Entities;
using VerticalSlice.PageProcessor.Features.ProcessPage.Services.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace VerticalSlice.PageProcessor.Features.ProcessPage;

/// <summary>
/// Эндпоинт для записи в БД. Реализован на FastEndpoints 
/// </summary>
public class ProcessPageEndpoint(IProcessPageService service,  IMapper mapper) : Endpoint<ProcessPageRequest, ProcessPageResponse>
{
    public override void Configure()
    {
       AllowAnonymous();
       Post("api/process-page");
       
       Summary(s =>
       {
           s.Summary = "Создание записи";
           s.Description = "Создание записи в БД из входного Json";
           s.Responses[204] = "Запись создана";
           s.Responses[400] = "Ошибка валидации Json";
           s.Responses[500] = "Внутренняя ошибка сервера";
       });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    public override async Task HandleAsync(ProcessPageRequest request,CancellationToken ct)
    {
        var entity = mapper.Map<ProcessPageRequestModel>(request);
        var result = await service.CreateRecordAsync(entity, ct);
        await Send.OkAsync(result, ct);
    }
}