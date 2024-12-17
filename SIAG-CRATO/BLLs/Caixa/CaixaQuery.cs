namespace SIAG_CRATO.BLLs.Caixa
{
    public class CaixaQuery
    {
        public const string SELECT = @"SELECT * FROM caixa ";

        public const string SELECT_BY_ID = @"SELECT * FROM caixa where id_caixa = @id";

    }
}
