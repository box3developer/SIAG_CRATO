using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.AreaArmzenagem;
using SIAG_CRATO.BLLs.Endereco;
using SIAG_CRATO.BLLs.Log;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.BLLs.Parametro;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.AreaArmazenagem;
using SIAG_CRATO.DTOs.Pallet;
using SIAG_CRATO.Integration;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AreaArmazenagem;

public class AreaArmazenagemBLL
{
    public static async Task<List<AreaArmazenagemDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemModel>(AreaArmazenagemQuery.SELECT);

        return areasArmazenagem.Select(ConvertToDTO).ToList();
    }

    public static async Task<AreaArmazenagemDTO?> GetByIdAsync(long id)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} WHERE id_areaarmazenagem = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { id });

        if (areasArmazenagem == null)
        {
            return null;
        }

        return ConvertToDTO(areasArmazenagem);
    }

    public static async Task<AreaArmazenagemDTO?> GetByAgrupadorAsync(int idAgrupador)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} WHERE id_agrupador = @idAgrupador";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { idAgrupador });

        if (areasArmazenagem == null)
        {
            return null;
        }

        return ConvertToDTO(areasArmazenagem);
    }

    public static async Task<bool> LiberarAreaArmazenagem(Guid idAgrupador, Guid? id_requisicao)
    {
        try
        {
            var logInitial = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = "",
                IdCaixa = "",
                Data = DateTime.Now,
                Mensagem = $"Liberando áreas de armazenagem do agrupador {idAgrupador}",
                Metodo = "FinalizarAgrupador",
                IdOperador = "",
                Tipo = "info",
            };

            await LogBLL.CreateLogCaracol(logInitial);

            using var conexao = new SqlConnection(Global.Conexao);

            var qtdLinhas = await conexao.ExecuteAsync(AreaArmazenagemQuery.UPDATE_LIBERA, new { idAgrupador });

            return qtdLinhas > 0;

        }
        catch (Exception ex)
        {
            var logError = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = "",
                IdCaixa = "",
                Data = DateTime.Now,
                Mensagem = $"Erro ao liberar áreas de armazenagem do agrupador {idAgrupador}",
                Metodo = "LiberarAreaArmazenagem",
                IdOperador = "",
                Tipo = "erro",
            };

            await LogBLL.CreateLogCaracol(logError);

            throw new Exception($"Erro ao liberar áreas de armazenagem do agrupador {idAgrupador}", ex);
        }
    }

    public static async Task<AreaArmazenagemDTO?> GetByPosicaoAsync(string identificadorCaracol, int posicaoY)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} 
                     WHERE CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                           AND nr_posicaoy = @posicaoY";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { identificadorCaracol, posicaoY });

        if (areasArmazenagem == null)
        {
            return null;
        }

        return ConvertToDTO(areasArmazenagem);
    }

    public static async Task<List<AreaArmazenagemDTO>> GetByIdentificadorCaracolAsync(string identificadorCaracol)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} 
                     WHERE CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                     ORDER BY nr_posicaoy DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemModel>(sql, new { identificadorCaracol });

        return areasArmazenagem.Select(ConvertToDTO).ToList();
    }

    public static async Task<int> SetStatusAsync(long id, StatusAreaArmazenagem status)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var area = await conexao.ExecuteAsync(AreaArmazenagemQuery.UPDATE_STATUS, new { status = (int)status, id });

        return area;
    }

    public static async Task<AreaArmazenagemDTO?> GetStageInLivreAsync(int idEndereco)
    {
        var parametroEntity = await ParametroBLL.GetParametroByParametro("TIPO AREA STAGEIN")
                                    ?? throw new Exception("Erro ao executar StageInLivre");

        var nmValor = short.Parse(parametroEntity.NmValor ?? "");

        var sql = $@"{AreaArmazenagemQuery.SELECT} where id_endereco = @idEndereco
		                                            and id_tipoarea = @nmValor
		                                            and fg_status = 1
		                                            order by nr_posicaoy, nr_lado";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { idEndereco, nmValor });

        if (areasArmazenagem == null)
        {
            return null;
        }

        return ConvertToDTO(areasArmazenagem);
    }

    public static List<StatusAreaArmazenagemDTO> GetTiposStatusGaiolas()
    {
        var listaTipos = new List<StatusAreaArmazenagemDTO>()
        {
            new ()
            {
                Cor = CorStatusAreaArmazenagem.Livre,
                Tipo = "Livre"
            },
            new ()
            {
                Cor = CorStatusAreaArmazenagem.Reservado,
                Tipo = "Reservado"
            },
            new()
            {
                Cor = CorStatusAreaArmazenagem.Ocupado,
                Tipo = "Ocupado"
            },
            new()
            {
                Cor = CorStatusAreaArmazenagem.Estufado,
                Tipo = "Estufado"
            },
            new()
            {
                Cor = CorStatusAreaArmazenagem.Bloqueado,
                Tipo = "Bloqueado"
            },
            new()
            {
                Cor = CorStatusAreaArmazenagem.Desabilitado,
                Tipo = "Desabilitado"
            },
            new()
            {
                Cor = CorStatusAreaArmazenagem.Manutencao,
                Tipo = "Manutencao"
                },
        };

        return listaTipos;
    }

    public static StatusAreaArmazenagemDTO GetStatusGaiola(AreaArmazenagemDTO areaArmazenagem, List<PalletDTO> pallets)
    {
        var retorno = new StatusAreaArmazenagemDTO();

        switch (areaArmazenagem.FgStatus)
        {
            case StatusAreaArmazenagem.Bloqueado:
                {
                    retorno.Cor = CorStatusAreaArmazenagem.Bloqueado;
                    retorno.Tipo = "Bloqueado";
                    break;
                }
            case StatusAreaArmazenagem.Desabilitado:
                {
                    retorno.Cor = CorStatusAreaArmazenagem.Desabilitado;
                    retorno.Tipo = "Desabilitado";
                    break;
                }
            case StatusAreaArmazenagem.Manutencao:
                {
                    retorno.Cor = CorStatusAreaArmazenagem.Manutencao;
                    retorno.Tipo = "Manutencao";
                    break;
                }
            case StatusAreaArmazenagem.Livre:
                {
                    retorno.Cor = CorStatusAreaArmazenagem.Livre;
                    retorno.Tipo = "Livre";
                    break;
                }
            case StatusAreaArmazenagem.Reservado:
                {
                    retorno.Cor = CorStatusAreaArmazenagem.Reservado;
                    retorno.Tipo = "Reservado";
                    break;
                }
            case StatusAreaArmazenagem.Ocupado:
                {
                    retorno.Cor = CorStatusAreaArmazenagem.Ocupado;
                    retorno.Tipo = "Ocupado";
                    break;
                }
        }

        if (areaArmazenagem.FgStatus == StatusAreaArmazenagem.Bloqueado ||
            areaArmazenagem.FgStatus == StatusAreaArmazenagem.Desabilitado ||
            areaArmazenagem.FgStatus == StatusAreaArmazenagem.Manutencao)
        {
            return retorno;
        }

        var pallet = pallets.Where(x => x.IdAreaArmazenagem == areaArmazenagem.IdAreaArmazenagem).FirstOrDefault();

        if (pallet == null)
        {
            retorno.SemPallet = true;
        }
        else if (pallet.FgStatus == StatusPallet.Cheio)
        {
            retorno.Cor = CorStatusAreaArmazenagem.Estufado;
        }

        retorno.Pallet = pallet?.IdPallet ?? 0;

        return retorno;
    }

    public static async Task<List<List<List<StatusAreaArmazenagemDTO>>>> GetStatusGaiolas(int idSetor)
    {
        var lista = new List<List<List<StatusAreaArmazenagemDTO>>>();

        if (idSetor <= 0)
        {
            throw new ArgumentException("Setor inválido!");
        }

        var enderecos = await EnderecoBLL.GetBySetor(idSetor);
        var pallets = await PalletBLL.GetListAsync();

        using var conexao = new SqlConnection(Global.Conexao);
        var sql = $"{AreaArmazenagemQuery.SELECT} WHERE id_endereco = @idEndereco ORDER BY nr_posicaox";

        var luzesVerdes = await NodeRedIntegration.GetAllLuzesVerdes();
        var luzesVermelhas = await NodeRedIntegration.GetAllLuzesVermelhas();

        var avenida = 100;
        foreach (var endereco in enderecos)
        {
            var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemDTO>(sql, new { idEndereco = endereco.IdEndereco });

            var listaAreasArmazenagem = areasArmazenagem.GroupBy(x => x.NrPosicaoX).Select(x => x.ToList()).ToList();
            var listaCaracol = new List<List<StatusAreaArmazenagemDTO>>();

            foreach (var areaArmazenagem in listaAreasArmazenagem)
            {
                var listaGaiolas = new List<StatusAreaArmazenagemDTO>();

                foreach (var gaiola in areaArmazenagem)
                {
                    var statusGaiola = GetStatusGaiola(gaiola, pallets);
                    statusGaiola.Caracol = gaiola.NrPosicaoX;
                    statusGaiola.Gaiola = gaiola.NrPosicaoY;
                    statusGaiola.Codigo = gaiola.IdAreaArmazenagem;

                    var luzes = luzesVermelhas.Where(x => x.Caracol == $"{avenida + statusGaiola.Caracol}").First();
                    var luzVerde = luzesVerdes.Where(x => x.Caracol == $"{avenida + statusGaiola.Caracol}").First();

                    statusGaiola.Status = luzes.LuzesVM[statusGaiola.Gaiola] == 0 ? StatusLuz.Desligado : StatusLuz.LuzVermelha;

                    if (luzVerde.LuzVerde != 0)
                    {
                        statusGaiola.Status = StatusLuz.LuzVerde;
                    }

                    listaGaiolas.Add(statusGaiola);
                }

                listaGaiolas = listaGaiolas.OrderBy(x => x.Gaiola).ToList();

                if (listaGaiolas.Count == 0)
                {
                    continue;
                }

                listaCaracol.Add(listaGaiolas);
            }

            lista.Add(listaCaracol);
            avenida += 100;
        }

        return lista;
    }

    private static AreaArmazenagemDTO ConvertToDTO(AreaArmazenagemModel areaArmazenagem)
    {
        return new()
        {
            IdAreaArmazenagem = areaArmazenagem.IdAreaArmazenagem,
            IdTipoArea = areaArmazenagem.IdTipoArea,
            IdEndereco = areaArmazenagem.IdEndereco,
            IdAgrupador = areaArmazenagem.IdAgrupador,
            IdCaracol = areaArmazenagem.IdCaracol,
            NrPosicaoX = areaArmazenagem.NrPosicaoX,
            NrPosicaoY = areaArmazenagem.NrPosicaoY,
            NrLado = areaArmazenagem.NrLado,
            FgStatus = areaArmazenagem.FgStatus,
            CdIdentificacao = areaArmazenagem.CdIdentificacao,
        };
    }
}
