using FastEndpoints;
using FluentValidation;

namespace VerticalSlice.PageProcessor.Features.ProcessPage;

/// <summary>
/// Валидация входного запроса. В FastEndpoints уже встроен FluentValidation
/// </summary>
public sealed class ProcessPageValidator : Validator<ProcessPageRequest>
{
    public ProcessPageValidator()
    {
        RuleFor(x => x.Selector)
            .NotEmpty()
            .WithMessage("Поле selector обязательно для заполнения.");

        RuleFor(x => x.Attribute)
            .NotEmpty()
            .WithMessage("Поле attribute обязательно для заполнения.");

        RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage("Поле url_b64 обязательно для заполнения.");

        RuleFor(x => x.EncryptedTextBytes)
            .NotEmpty()
            .WithMessage("Поле encrypted_text_bytes_b64 обязательно для заполнения.");

        RuleFor(x => x.KeyBytes)
            .NotEmpty()
            .WithMessage("Поле key_bytes_b64 обязательно для заполнения.");

        RuleFor(x => x.Page)
            .NotEmpty()
            .WithMessage("Поле page_b64 обязательно для заполнения.");
    }
}