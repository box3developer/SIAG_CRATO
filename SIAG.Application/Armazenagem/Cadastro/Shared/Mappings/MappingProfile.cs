using AutoMapper;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Application.Armazenagem.Cadastro.Shared.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AreaArmazenagem, AreaArmazenagemDTO>().ReverseMap();
            CreateMap<Deposito, DepositoDTO>().ReverseMap();
            CreateMap<TipoEndereco, TipoEnderecoDTO>().ReverseMap();
            CreateMap<SetorTrabalho, SetorTrabalhoDTO>().ReverseMap();
            CreateMap<RegiaoTrabalho, RegiaoTrabalhoDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<TipoArea, TipoAreaDTO>().ReverseMap();
            CreateMap<AgrupadorAtivo, AgrupadorAtivoDTO>().ReverseMap();
            CreateMap<Pallet, PalletDTO>().ReverseMap();
            CreateMap<Programa, ProgramaDTO>().ReverseMap();
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<Caixa, CaixaDTO>().ReverseMap();
            CreateMap<Equipamento, EquipamentoDTO>().ReverseMap();
            CreateMap<Turno, TurnoDTO>().ReverseMap();
            // Adicione outros mapeamentos aqui
        }
    }
}
