namespace SIAG.CrossCutting.DTOs
{
    public class SelectDTO<T>
    {
        public T? Id { get; set; }
        public string? Descricao { get; set; } = string.Empty;
    }
}
