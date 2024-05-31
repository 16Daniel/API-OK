using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.rebel_wings.Models.InventarioMensual
{
    public class InventarioMensual
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public int City { get; set; }
        public int Sucursal { get; set; }
        public int Codarticulo { get; set; }
        public string Referencia { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Medida { get; set; } = null!;
        public decimal? Unidades { get; set; }
        public decimal? StockAnterior { get; set; }
        public decimal? Diferencia { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Precio { get; set; }
        public DateTime Date { get; set; }
        public bool Procesado { get; set; }
        public int? orden { get; set; }
        public string? tipo { get; set; }
        public decimal? ValorDif { get; set; }
    }
}
