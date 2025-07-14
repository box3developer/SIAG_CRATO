namespace SIAG_CRATO.Services;

public class ValidacaoEnderecoResult
{
    public bool IsValid { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}

public class EnderecoLeituraDto
{
    public long IdAreaArmazenagemOrigem { get; set; }
    public long IdAreaArmazenagemDestino { get; set; }
    public long? IdAreaArmazenagemLeitura { get; set; }
    public int? IdPalletLeitura { get; set; }
    public int? IdPalletOrigem { get; set; }
    public int? IdPalletDestino { get; set; }
    public long? IdOperador { get; set; }
    public int IdAtividade { get; set; }
}

public class ChamadaLivreDto
{
    public long? IdAreaArmazenagemLeitura { get; set; }
    public int? IdPalletLeitura { get; set; }
    public long IdAreaArmazenagemOrigem { get; set; }
    public long? IdOperador { get; set; }
    public int IdAtividade { get; set; }
}

public class ArmazenarPalletDto
{
    public long IdAreaOrigem { get; set; }
    public long IdAreaDestino { get; set; }
    public int? IdPalletOrigem { get; set; }
    public long? IdOperador { get; set; }
    public int IdAtividade { get; set; }
}

public class AreaInfoDto
{
    public int IdEndereco { get; set; }
    public int NrPosY { get; set; }
    public int NrLado { get; set; }
}


