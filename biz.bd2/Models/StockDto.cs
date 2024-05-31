using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.bd2.Models
{
    public class StockDto
    {
        public string Codalmacen { get; set; }
        public string Descripcion { get; set; }
        public int Codarticulo { get; set; }
        public string? Regulariza { get; set; }
        public string? Unidadessat { get; set; }
        public string? Unidadmedida { get; set; }
        public double? Stock1 { get; set; }
        public DateTime? Ultfecha { get; set; }
        public string? RegularizaSemanal { get; set; }
        public string? InventarioMensual { get; set; }
        public int? Orden { get; set; }
    }
    public class getArticulo 
    { 
         public decimal? stockAnt { get; set; }
         public decimal? precio { get; set; }
         public string Descripcion { get; set; }
         public string Referencia { get; set; }
         public string Medida { get; set; }
         public int? orden { get; set; }
         public string? tipo { get; set; }
    }
}
