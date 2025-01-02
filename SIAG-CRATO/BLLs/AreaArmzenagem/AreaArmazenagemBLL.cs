using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Endereco;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.BLLs.Parametro;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.AreaArmazenagem;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AreaArmzenagem;

public class AreaArmazenagemBLL
{
    public static async Task<List<AreaArmazenagemModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemModel>(AreaArmazenagemQuery.SELECT);

        return areasArmazenagem.ToList();
    }

    public static async Task<AreaArmazenagemModel?> GetByIdAsync(long id)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} WHERE id_areaarmazenagem = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { id });

        return areasArmazenagem;
    }


    public static async Task<AreaArmazenagemModel?> GetByAgrupadorAsync(int idAgrupador)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} WHERE id_agrupador = @idAgrupador";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { idAgrupador });

        return areasArmazenagem;
    }

    public static async Task<AreaArmazenagemModel?> GetByPosicaoAsync(string identificadorCaracol, int posicaoY)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} 
                     WHERE CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                           AND nr_posicaoy = @posicaoY";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { identificadorCaracol, posicaoY });

        return areasArmazenagem;
    }

    public static async Task<List<AreaArmazenagemModel>> GetByIdentificadorCaracolAsync(string identificadorCaracol)
    {
        var sql = $@"{AreaArmazenagemQuery.SELECT} 
                     WHERE CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                     ORDER BY nr_posicaoy DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemModel>(sql, new { identificadorCaracol });

        return areasArmazenagem.ToList();
    }

    public static async Task<int> SetStatusAsync(long id, StatusAreaArmazenagem status)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var area = await conexao.ExecuteAsync(AreaArmazenagemQuery.UPDATE_STATUS, new { status = (int)status, id });

        return area;
    }

    public static async Task<AreaArmazenagemModel?> GetStageInLivreAsync(int idEndereco)
    {
        var parametroEntity = await ParametroBLL.GetParametroByParametro("TIPO AREA STAGEIN")
        ??
            throw new Exception("Erro ao executar StageInLivre");

        var nmValor = Int16.Parse(parametroEntity.Valor ?? "");

        var sql = $@"{AreaArmazenagemQuery.SELECT} where id_endereco = @idEndereco
		                                            and id_tipoarea = @nmValor
		                                            and fg_status = 1
		                                            order by nr_posicaoy, nr_lado";

        using var conexao = new SqlConnection(Global.Conexao);
        var areasArmazenagem = await conexao.QueryFirstOrDefaultAsync<AreaArmazenagemModel>(sql, new { idEndereco, nmValor });

        return areasArmazenagem;

    }

    public static List<StatusAreaArmazenagemDTO> GetTiposStatusGaiolas()
    {
        var listaTipos = new List<StatusAreaArmazenagemDTO>()
            {
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
                }
            };

        return listaTipos;
    }

    public static async Task<StatusAreaArmazenagemDTO> GetStatusGaiola(AreaArmazenagemModel areaArmazenagem)
    {
        var retorno = new StatusAreaArmazenagemDTO();

        switch (areaArmazenagem.Status)
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

        if (areaArmazenagem.Status == StatusAreaArmazenagem.Bloqueado ||
            areaArmazenagem.Status == StatusAreaArmazenagem.Desabilitado ||
            areaArmazenagem.Status == StatusAreaArmazenagem.Manutencao)
        {
            return retorno;
        }

        var pallet = await PalletBLL.GetByAreaArmazenagemAsync(areaArmazenagem.Codigo);

        if (pallet == null)
        {
            retorno.SemPallet = true;
        }
        else if (pallet.Status == StatusPallet.Cheio)
        {
            retorno.Cor = CorStatusAreaArmazenagem.Estufado;
        }

        retorno.Pallet = pallet?.Codigo ?? 0;

        return retorno;
    }

    public static async Task<List<List<StatusAreaArmazenagemDTO>>> GetStatusGaiolas(int idSetor)
    {
        var lista = new List<List<StatusAreaArmazenagemDTO>>();

        if (idSetor <= 0)
        {
            throw new ArgumentException("Setor inválido!");
        }

        var enderecos = await EnderecoBLL.GetBySetor(idSetor);

        using var conexao = new SqlConnection(Global.Conexao);
        var sql = $"{AreaArmazenagemQuery.SELECT} WHERE id_endereco = @idEndereco ORDER BY nr_posicaox";

        foreach (var endereco in enderecos)
        {

            var areasArmazenagem = await conexao.QueryAsync<AreaArmazenagemModel>(sql, new { idEndereco = endereco.IdEndereco });

            var listaGaiolas = new List<StatusAreaArmazenagemDTO>();

            foreach (var areaArmazenagem in areasArmazenagem)
            {
                var statusGaiola = await GetStatusGaiola(areaArmazenagem);
                statusGaiola.Caracol = areaArmazenagem.PosicaoX;
                statusGaiola.Gaiola = areaArmazenagem.PosicaoY;
                statusGaiola.Codigo = areaArmazenagem.Codigo;

                listaGaiolas.Add(statusGaiola);
            }

            listaGaiolas = listaGaiolas.OrderBy(x => x.Gaiola).ToList();

            if (listaGaiolas.Count == 0)
            {
                continue;
            }

            lista.Add(listaGaiolas);
        }

        return lista;
    }
}
