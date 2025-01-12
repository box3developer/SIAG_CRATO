namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class DesempenhoCaixaDTO
    {
        public string? IdCaixa { get; set; } = string.Empty;

        public DateTime? DtLeituracaixa { get; set; }

        public int? FgErroclassificacao { get; set; }


        public long? IdAreaArmazenagem { get; set; }

        public AreaArmazenagemDTO? AreaArmazenagem { get; set; }


        public int? IdEquipamento { get; set; }

        public EquipamentoDTO? Equipamento { get; set; }


        public long? IdOperador { get; set; }

        public OperadorDTO? Operador { get; set; }


        public int? NrTempoestimado { get; set; }
    }
}
