namespace api.rebel_wings.Models.Stock
{
    public class StockDto
    {
        public string Codalmacen { get; set; }
        public string Descripcion { get; set; }
        public int Codarticulo { get; set; }
        public string? Regulariza { get; set; }
        public string? Unidadessat { get; set; }
        public string? Unidadmedida { get; set; }
        public string? RegularizaSemanal { get; set; }
        public string? InventarioMensual { get; set; }
        public int? Orden { get; set; }
    }
    public class TipoInvDto
    {

        public int? IdSucursal { get; set; }
    }
}
