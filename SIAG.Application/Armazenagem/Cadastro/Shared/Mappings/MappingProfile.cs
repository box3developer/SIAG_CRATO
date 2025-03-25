using AutoMapper;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.Application.Armazenagem.Cadastro.Shared.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AgrupadorAtivoDTO, AgrupadorAtivo>().ReverseMap();
            CreateMap<AreaArmazenagemDTO, AreaArmazenagem>().ReverseMap();
            CreateMap<AtividadeDTO, Atividade>().ReverseMap();
            CreateMap<CaixaDTO, Caixa>().ReverseMap();
            CreateMap<CaixaLeituraDTO, CaixaLeitura>().ReverseMap();
            CreateMap<ChamadaDTO, Chamada>().ReverseMap();
            CreateMap<DepositoDTO, Deposito>().ReverseMap();
            CreateMap<DesempenhoDTO, Desempenho>().ReverseMap();
            CreateMap<DesempenhoCaixaDTO, DesempenhoCaixa>().ReverseMap();
            CreateMap<EnderecoDTO, Endereco>().ReverseMap();
            CreateMap<EquipamentoDTO, Equipamento>().ReverseMap();
            CreateMap<EquipamentoModeloDTO, EquipamentoModelo>().ReverseMap();
            CreateMap<LiderVirtualDTO, LiderVirtual>().ReverseMap();
            CreateMap<NiveisAgrupadoresDTO, NiveisAgrupadores>().ReverseMap();
            CreateMap<OperadorDTO, Operador>().ReverseMap();
            CreateMap<OperadorHistoricoDTO, OperadorHistorico>().ReverseMap();
            CreateMap<PalletDTO, Pallet>().ReverseMap();
            CreateMap<ParametroDTO, Parametro>().ReverseMap();
            CreateMap<ParametroMensagemCaracolDTO, ParametroMensagemCaracol>().ReverseMap();
            CreateMap<PedidoDTO, Pedido>().ReverseMap();
            CreateMap<PosicaoCaracolRefugoDTO, PosicaoCaracolRefugo>().ReverseMap();
            CreateMap<ProgramaDTO, Programa>().ReverseMap();
            CreateMap<RegiaoTrabalhoDTO, RegiaoTrabalho>().ReverseMap();
            CreateMap<SetorTrabalhoDTO, SetorTrabalho>().ReverseMap();
            CreateMap<StatusLeitorDTO, StatusLeitor>().ReverseMap();
            CreateMap<TipoAreaDTO, TipoArea>().ReverseMap();
            CreateMap<TipoEnderecoDTO, TipoEndereco>().ReverseMap();
            CreateMap<TurnoDTO, Turno>().ReverseMap();
            CreateMap<LogCaracolDTO, LogCaracol>().ReverseMap();
            CreateMap<TempoAtividadeDTO, TempoAtividade>().ReverseMap();
            // Adicione outros mapeamentos aqui
        }
    }
}