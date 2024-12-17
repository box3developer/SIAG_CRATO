namespace SIAG_CRATO.BLLs.Endereco
{
    public class EnderecoQuery
    {
        public const string SELECT = @"SELECT 
                                        id_endereco,
                                        id_regiaotrabalho,
                                        id_setortrabalho,
                                        id_tipoendereco,
                                        nm_endereco,
                                        qt_estoqueminimo,
                                        qt_estoquemaximo,
                                        fg_status,
                                        tp_preenchimento
                                   FROM endereco with(NOLOCK)";
    }
}
