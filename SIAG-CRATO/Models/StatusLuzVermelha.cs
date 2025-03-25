using System.Text.Json.Serialization;

namespace SIAG_CRATO.Models
{
    public class StatusLuzVermelha
    {
        [JsonPropertyName("caracol")]
        public string Caracol { get; set; } = string.Empty;

        [JsonPropertyName("luzesVM")]
        public List<int?> LuzesVM { get; set; } = new();
    }
}
