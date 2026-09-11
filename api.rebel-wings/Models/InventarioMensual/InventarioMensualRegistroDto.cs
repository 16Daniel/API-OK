namespace api.rebel_wings.Models.InventarioMensual
{
    public class InventarioMensualRegistroDto
    {
        public int? Id { get; set; }
        public int? City { get; set; }
        public int? Sucursal { get; set; }
        public string? Captura { get; set; } = null!;
        public DateTime? DateCaptura { get; set; }
        public bool? Procesado { get; set; }
    }
}
