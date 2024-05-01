using biz.rebel_wings.Repository.Generic;

namespace biz.rebel_wings.Repository.InventarioMensual
{
    public interface IInventarioMensualRepository : IGenericRepository<Entities.InventarioMensual>
    {
        biz.rebel_wings.Models.InventarioMensual.InventarioMensual CreaCaptura(int city, int sucursal, int codarticulo, decimal? unidades, decimal? precio, decimal? stockant,string referencia,string medida, string descripcion, int registro);
        List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual> getCapturas(int registro);
        biz.rebel_wings.Models.InventarioMensual.InventarioMensual modificaCapturas(int idcaptura, decimal unidades);
        Boolean procesadoCapturas(int idcaptura);
        List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual> getCapturasExcel(int registro, string sucursal, string correo);
    }
}
