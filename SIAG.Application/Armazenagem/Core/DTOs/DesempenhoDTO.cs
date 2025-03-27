using SIAG.Application.Armazenagem.Cadastro.DTOs;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class DesempenhoDTO
    {
        public long IdDesempenho { get; set; }

        public DateTime DtCadastro { get; set; }

        public int? FgErroclassificacao { get; set; }

        public int? FgHumoreficiencia { get; set; }


        public long? IdAreaArmazenagem { get; set; }

        public AreaArmazenagemDTO? AreaArmazenagem { get; set; }


        public int? IdEquipamento { get; set; }

        public EquipamentoDTO? Equipamento { get; set; }


        public int? IdEquipamentoModelo { get; set; }

        public EquipamentoModeloDTO? EquipamentoModelo { get; set; }


        public long? IdOperador { get; set; }

        public OperadorDTO? Operador { get; set; }


        public string? IdReferencia { get; set; } = string.Empty;


        public int? IdSetorTrabalho { get; set; }

        public SetorTrabalhoDTO? SetorTrabalho { get; set; }


        public int? NrTempoestimado { get; set; }

        public int? NrTemporealizado { get; set; }
    }
}
