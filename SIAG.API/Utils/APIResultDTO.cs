namespace SIAG.API.Utils
{
    public class APIResultDTO
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
        public string Tipo { get; set; } // ALERTA, ERRO
    }
}
