using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models
{
    public class EnderecoModel
    {
        public int Codigo { get; set; }
        public RegiaoModel Regiao { get; set; }
        public SetorModel Setor { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public string Descricao { get; set; }
        public int EstoqueMinimo { get; set; }
        public int EstoqueMaximo { get; set; }
        public StatusEndereco Status { get; set; }
        public TipoPreenchimento TipoPreenchimento { get; set; }
    }

    public class TipoEndereco
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }

    public class RegiaoModel
    {
        public int Codigo { get; set; }
        public DepositoModel Deposito { get; set; }
        public string Descricao { get; set; }
    }

    public class SetorModel
    {

        public int Codigo { get; set; }
        public DepositoModel? Deposito { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }

    public class DepositoModel
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }

}
