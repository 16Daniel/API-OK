using biz.rebel_wings.Repository.Generic;

namespace biz.rebel_wings.Repository.InventarioMensual
{ 
    public interface IInventarioMensualRegistroRepository : IGenericRepository<Entities.InventarioMensualRegistro>
    {
            biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro CreaRegistro(int city, int sucursal, string captura);
            biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro GetRegistro(int sucursal);
            Boolean procesadoRegistro(int registro);
    }
}