using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Desempenho;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.BLLs.EquipamentoManutencao;
using SIAG_CRATO.BLLs.OperadorHistorico;
using SIAG_CRATO.BLLs.Parametro;
using SIAG_CRATO.BLLs.Turno;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Operador;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Operador;

public class OperadorBLL
{
    public static async Task<OperadorDTO?> GetByCrachaAsync(string cracha)
    {
        var sql = $"{OperadorQuery.SELECT} WHERE id_operador = @cracha";

        using var conexao = new SqlConnection(Global.Conexao);
        var operador = await conexao.QueryFirstOrDefaultAsync<OperadorModel>(sql, new { cracha });

        if (operador == null)
        {
            return null;
        }

        return ConvertToDTO(operador);
    }

    public static async Task<OperadorDTO?> GetByNFCAsync(string nfc)
    {
        var sql = $"{OperadorQuery.SELECT} WHERE nm_nfcoperador = @nfc";

        using var conexao = new SqlConnection(Global.Conexao);
        var operador = await conexao.QueryFirstOrDefaultAsync<OperadorModel>(sql, new { nfc });

        if (operador == null)
        {
            return null;
        }

        return ConvertToDTO(operador);
    }

    public static async Task<int> GetPerformance(long idOperador)
    {
        var turnos = await TurnoBLL.GetListPerformance();

        var turno = turnos.Where(x => x.DtInicio <= DateTime.Now && x.DtInicio >= DateTime.Now).LastOrDefault();

        if (turno == null)
        {
            turno = new()
            {
                DtInicio = DateTime.Now.AddHours(-6),
                DtFim = DateTime.Now,
            };
        }

        double caixaHora = await ParametroBLL.GetParametroValorByParametro(Parametros.CAIXA_HORA);
        double patinhaHora = await ParametroBLL.GetParametroValorByParametro(Parametros.PATINHA_HORA);
        double retratilHora = await ParametroBLL.GetParametroValorByParametro(Parametros.RETRATIL_HORA);
        double bilateralHora = await ParametroBLL.GetParametroValorByParametro(Parametros.BILATERAL_HORA);

        var equipamento = await EquipamentoBLL.GetByOperadorAsync(idOperador.ToString());

        if (equipamento == null || equipamento.IdEquipamentoModelo == 0 || (equipamento.IdSetorTrabalho ?? 0) == 0)
        {
            return 1;
        }

        var desempenhos = await DesempenhoBLL.GetByPerformance(idOperador, equipamento.IdSetorTrabalho ?? 0, equipamento.IdEquipamentoModelo);

        var quantidadeEstimada = desempenhos.Select(x =>
        {
            double fatorHora = (double)x.NrPeriodo / 3600;
            double parametro = x.IdSetorTrabalho switch
            {
                1 when x.IdEquipamentoModelo == 1 => caixaHora,
                1 when x.IdEquipamentoModelo == 2 => patinhaHora,
                2 when x.IdEquipamentoModelo == 3 => retratilHora,
                3 when x.IdEquipamentoModelo == 4 => bilateralHora,
                4 when x.IdEquipamentoModelo == 3 => retratilHora,
                _ => 0
            };

            return fatorHora * parametro;
        }).LastOrDefault();

        var quantidadeOperacoes = desempenhos.Select(x => (double)x.NrOperacoesRealizadas).LastOrDefault();

        var calculo = quantidadeOperacoes / quantidadeEstimada * 100;

        if (calculo == 0)
        {
            return 0;
        }

        if (calculo < await ParametroBLL.GetParametroValorByParametro(Parametros.EFICIENCIA_REGULAR))
        {
            return 0;
        }

        if (calculo < await ParametroBLL.GetParametroValorByParametro(Parametros.EFICIENCIA_FELIZ))
        {
            return 1;
        }

        return 2;
    }

    public static async Task<int> GetMetaAsync()
    {
        var sql = $"{OperadorQuery.SELECT_PARAMETRO} WHERE nm_parametro = 'Caixa hora operador sorter'";

        using var conexao = new SqlConnection(Global.Conexao);
        var meta = await conexao.QueryFirstOrDefaultAsync<int>(sql);

        return meta;
    }

    public static async Task<bool> Login(int id_operador, int id_equipamento)
    {

        await LogOff(id_operador, id_equipamento);

        await EquipamentoBLL.SetEquipamentoOperador(id_operador, id_equipamento);
        await EquipamentoManutencaoBLL.SetDtFimByEquipamento(id_equipamento);
        await OperadorHistoricoBLL.SetHistorico(id_operador, id_equipamento);
        //....sp_siag_alocacaoautomaticabilaterais
        return true;
    }

    public static async Task<bool> LogOff(int id_operador, int id_equipamento)
    {
        if (id_operador == 0 || id_equipamento == 0)
        {
            return false;
        }
        var getEquip = await EquipamentoBLL.GetByOperadorAsync(id_operador.ToString()) ?? await EquipamentoBLL.GetByIdAsync(id_equipamento);

        if (getEquip == null)
        {
            return false;
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var result = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_EQUIPAMENTO_OPERADOR_LOGOFF, new { id_equipamento = getEquip.IdEquipamento });

        //....sp_siag_alocacaoautomaticabilaterais
        return result > 0;
    }

    private static OperadorDTO ConvertToDTO(OperadorModel operador)
    {
        return new()
        {
            IdOperador = operador.IdOperador,
            NFC = operador.NmNfcOperador,
            NmCpf = operador.NmCpf,
            NmOperador = operador.NmOperador,
            DtLogin = operador.DtLogin,
            NrLocalidade = operador.NrLocalidade,
            FgFuncao = operador.FgFuncao,
            IdResponsavel = operador.IdResponsavel,
        };
    }
}
