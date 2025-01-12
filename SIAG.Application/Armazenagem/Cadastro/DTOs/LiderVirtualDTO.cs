namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class LiderVirtualDTO
    {
        public long IdLidervirtual { get; set; }

        public DateTime? DtLogin { get; set; }

        public DateTime? DtLoginlimite { get; set; }

        public DateTime? DtLogoff { get; set; }

        public int? IdEquipamentoDestino { get; set; }

        public EquipamentoDTO? EquipamentoDestino { get; set; }


        public int? IdEquipamentoOrigem { get; set; }

        public EquipamentoDTO? EquipamentoOrigem { get; set; }


        public long? IdOperador { get; set; }

        public OperadorDTO? Operador { get; set; }


        public long? IdOperadorlogin { get; set; }

        public OperadorDTO? OperadorLogin { get; set; }
    }
}
