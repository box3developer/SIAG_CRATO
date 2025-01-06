using SIAG_CRATO.Models;

namespace SIAG_CRATO.Integration;

public class NodeRedIntegration
{
    private static readonly HttpClient client = new();
    private static readonly string url = Global.NodeRedAPI;

    public static async Task<bool> Sincronizar()
    {
        try
        {
            await client.GetAsync($"{url}/sync");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<List<StatusLuzVermelha>> GetAllLuzesVermelhas()
    {
        try
        {
            var listaStatus = await client.GetFromJsonAsync<List<StatusLuzVermelha>>($"{url}/allvermelhas");

            return listaStatus ?? [];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<List<StatusLuzVerde>> GetAllLuzesVerdes()
    {
        try
        {
            var listaStatus = await client.GetFromJsonAsync<List<StatusLuzVerde>>($"{url}/allverdes");

            return listaStatus ?? [];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> AcenderLuzVermelha(int caracol, int gaiola)
    {
        try
        {
            await client.GetAsync($"{url}/vm/{caracol}/{gaiola}");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> AcenderLuzVerde(int caracol, int gaiola)
    {
        try
        {
            await client.GetAsync($"{url}/vd/{caracol}/{gaiola}");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> ApagarLuzVermelha(int caracol, int gaiola)
    {
        try
        {
            await client.GetAsync($"{url}/apagaluzvm/{caracol}/{gaiola}");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> LigarTodasLuzVermelha()
    {
        try
        {
            await client.GetAsync($"{url}/red-lights-on");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> DesligarTodasLuzVermelha()
    {
        try
        {
            await client.GetAsync($"{url}/red-lights-off");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> DesligarTodasLuzVerdes()
    {
        try
        {
            await client.GetAsync($"{url}/green-lights-off");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
