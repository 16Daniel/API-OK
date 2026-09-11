using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.rebel_wings.Models.InventarioMensual
{
    public class InventarioMensualRegistro
    {
        public int? Id { get; set; }
        public int City { get; set; }
        public int Sucursal { get; set; }
        public string Captura { get; set; } = null!;
        public DateTime DateCaptura { get; set; }
        public bool Procesado { get; set; }
    }
}
