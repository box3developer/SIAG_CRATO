namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class ChamadaDTO
    {
        public Guid IdChamada { get; set; }


        public int IdPalletorigem { get; set; }

        public PalletDTO? Palletorigem { get; set; }


        public long IdAreaarmazenagemorigem { get; set; }

        public AreaArmazenagemDTO? Areaarmazenagemorigem { get; set; }


        public int IdPalletDestino { get; set; }

        public PalletDTO? PalletDestino { get; set; }


        public long IdAreaarmazenagemdestino { get; set; }

        public AreaArmazenagemDTO? Areaarmazenagemdestino { get; set; }


        public int IdPalletleitura { get; set; }

        public PalletDTO? Palletleitura { get; set; }


        public long IdAreaarmazenagemleitura { get; set; }

        public AreaArmazenagemDTO? Areaarmazenagemleitura { get; set; }


        public long IdOperador { get; set; }

        public OperadorDTO? Operador { get; set; }


        public int IdEquipamento { get; set; }

        public EquipamentoDTO? Equipamento { get; set; }


        public int IdAtividaderejeicao { get; set; }

        public AtividadeDTO? Atividaderejeicao { get; set; }


        public int IdAtividade { get; set; }

        public AtividadeDTO? Atividade { get; set; }


        public int FgStatus { get; set; }

        public DateTime DtChamada { get; set; }

        public DateTime DtAtendida { get; set; }

        public DateTime DtFinalizada { get; set; }

        public DateTime DtRecebida { get; set; }

        public DateTime DtRejeitada { get; set; }


        public Guid IdChamadaorigem { get; set; }

        public ChamadaDTO? Chamadaorigem { get; set; }


        public Guid IdChamadasuspensa { get; set; }

        public ChamadaDTO? Chamadasuspensa { get; set; }


        public bool Priorizar { get; set; }
    }
}
