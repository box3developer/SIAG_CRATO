﻿namespace SIAG_CRATO.BLLs.AreaArmzenagem;

public class AreaArmazenagemQuery
{
    public const string SELECT = @"SELECT 
                                        CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) as id_caracol, 
                                        id_endereco,
                                        id_tipoarea,
                                        id_areaarmazenagem, 
                                        id_agrupador, 
                                        id_agrupador_reservado, 
                                        fg_status,
                                        nr_lado,
                                        cd_identificacao,
                                        nr_posicaox, 
                                        nr_posicaoy 
                                   FROM areaarmazenagem WITH(NOLOCK)";

    public const string UPDATE_STATUS = @"UPDATE areaarmazenagem SET fg_status = @status WHERE id_areaarmazenagem = @id";

    public const string UPDATE_LIBERA = @"UPDATE areaarmazenagem SET id_agrupador = NULL, fg_status = 1 WHERE id_agrupador = @idAgrupador";
}
