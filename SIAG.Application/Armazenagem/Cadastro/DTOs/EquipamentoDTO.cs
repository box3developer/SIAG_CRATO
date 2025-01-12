namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class EquipamentoDTO
    {
        public int IdEquipamento { get; set; }

        public int? IdEquipamentoModelo { get; set; }

        public EquipamentoModeloDTO? EquipamentoModelo { get; set; }


        public int? IdSetorTrabalho { get; set; }
        
        public SetorTrabalhoDTO? SetorTrabalho { get; set; }


        public long? IdOperador { get; set; }

        public OperadorDTO? Operador { get; set; }


        public long? IdOperador2 { get; set; }

        public OperadorDTO? Operador2 { get; set; }


        public int? IdEndereco { get; set; }

        public EnderecoDTO? Endereco { get; set; }


        public string? NmEquipamento { get; set; } = string.Empty;

        public string? NmIdentificador { get; set; } = string.Empty;

        public int? FgStatus { get; set; }

        public DateTime? DtInclusao { get; set; }

        public DateTime? DtManutencao { get; set; }

        public string? NmObservacao { get; set; } = string.Empty;

        public string? CdUltimaLeitura { get; set; } = string.Empty;

        public DateTime? DtUltimaLeitura { get; set; }

        public string? NmIp { get; set; } = string.Empty;

        public int? FgStatusTrocaCaracol { get; set; }

        public string? NmAbreviadoEquipamento { get; set; } = string.Empty;

        public string? CdLeituraPendente { get; set; } = string.Empty;

        public string? NmUsuarioLiberacao { get; set; } = string.Empty;
    }
}
