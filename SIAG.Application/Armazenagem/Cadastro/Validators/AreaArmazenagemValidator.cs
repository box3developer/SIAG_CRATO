using FluentValidation;
using SIAG.Application.Armazenagem.Core.DTOs;

namespace SIAG.Application.Armazenagem.Cadastro.Validators
{
    public class AreaArmazenagemValidator : AbstractValidator<AreaArmazenagemDTO>
    {
        public AreaArmazenagemValidator()
        {
            RuleFor(x => x.IdTipoArea)
                .NotNull().WithMessage("Preencha o campo 'Tipo da Área de Armazenagem' com um valor válido.")
                .GreaterThan(0).WithMessage("O Tipo da Área deve ser maior que 0.");

            RuleFor(x => x.IdEndereco)
                .NotNull().WithMessage("Preencha o campo 'Endereço da Área de Armazenagem' com um valor válido.")
                .GreaterThan(0).WithMessage("O Endereço deve ser maior que 0.");
        }
    }
}
