namespace SIAG_CRATO.BLLs.StatusLeitor;

public class StatusLeitorQuery
{
    public const string SELECT = "SELECT * FROM status_leitor WITH(NOLOCK)";
    public const string INSERT = "INSERT INTO status_leitor(equipamento, leitor, configurado, conectado, executando, dt_status) VALUES (@equipamento, @leitor, @configurado, @conectado, @executando, @dataStatus)";
    public const string UPDATE = "UPDATE status_leitor SET conectado = @conectado, configurado = @configurado, executando = @executando, dt_status = @dataStatus WHERE equipamento = @idEquipamento AND leitor = @idLeitor";
}
