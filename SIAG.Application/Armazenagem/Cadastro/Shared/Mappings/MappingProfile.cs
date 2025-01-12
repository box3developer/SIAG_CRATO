using AutoMapper;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Application.Armazenagem.Cadastro.Shared.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AreaArmazenagemDTO, AreaArmazenagemDTO>().ReverseMap();
            CreateMap<Deposito, DepositoDTO>().ReverseMap();
            CreateMap<TipoEnderecoDTO, TipoEnderecoDTO>().ReverseMap();
            CreateMap<SetorTrabalhoDTO, SetorTrabalhoDTO>().ReverseMap();
            CreateMap<RegiaoTrabalhoDTO, RegiaoTrabalhoDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<TipoAreaDTO, TipoAreaDTO>().ReverseMap();
            CreateMap<AgrupadorAtivoDTO, AgrupadorAtivoDTO>().ReverseMap();
            CreateMap<PalletDTO, PalletDTO>().ReverseMap();
            CreateMap<ProgramaDTO, ProgramaDTO>().ReverseMap();
            CreateMap<PedidoDTO, PedidoDTO>().ReverseMap();
            CreateMap<Caixa, CaixaDTO>().ReverseMap();
            CreateMap<Equipamento, EquipamentoDTO>().ReverseMap();
            CreateMap<TurnoDTO, TurnoDTO>().ReverseMap();
            // Adicione outros mapeamentos aqui
        }
    }
}
