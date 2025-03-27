namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class OrdemDTO
    {
        public int IdTransportadora { get; set; }

        public int IdVeiculo { get; set; }

        public int IdMotorista { get; set; }

        public int IdEndereco { get; set; }

        public decimal QtCubagem { get; set; }

        public DateTime DtGeracao { get; set; }

        public DateTime DtPrevisao { get; set; }

        public DateTime DtEntrada { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtFim { get; set; }

        public DateTime DtSaida { get; set; }

        public int FgControleendereco { get; set; }

        public int FgControlesms { get; set; }

        public int IdMotoristamanobrista { get; set; }

        public string CdCentrocusto { get; set; }

        public decimal NrCustovalor { get; set; }

        public int IdSolicitante { get; set; }

        public int IdMotivo { get; set; }

        public bool Priorizar { get; set; }
    }
}
