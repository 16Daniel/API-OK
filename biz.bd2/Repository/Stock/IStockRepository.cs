using biz.bd2.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.bd2.Repository.Stock
{
    public interface IStockRepository : IGenericRepository<biz.bd2.Entities.Stock>
    {
        biz.bd2.Models.TipoInvDto GetTipoInv(int id_sucursal);
        List<biz.bd2.Models.StockDto> GetStock(int id_sucursal);
        List<biz.bd2.Models.StockDto> GetStockV(int id_sucursal);
        List<biz.bd2.Models.StockDto> GetStockM(int id_sucursal);
        decimal StockValidate(int id_sucursal, int codarticulo);
        decimal StockValidateV(int id_sucursal, int codarticulo);
        biz.bd2.Models.StockDto UpdateStock(int codArticulo, string codAlmacen, double cantidad);
        biz.bd2.Models.StockDto UpdateStockV(int codArticulo, string codAlmacen, double cantidad);
        Boolean UpdateStockInv(int codArticulo, string codAlmacen,double cantidad);
        String ObtenerAlmacen(int idsucursal);
        List<biz.bd2.Models.Mermas> GetMermas(int branch, DateTime initDate, DateTime endDate);
        List<biz.bd2.Models.Ranking> GetRkg(string suc, DateTime initDate, DateTime endDate);
        List<biz.bd2.Models.Reporte> GetReporte(DateTime Date);
        List<biz.bd2.Models.Reporte> GetReporteV(DateTime Date);
        List<biz.bd2.Models.Apps> GetReporteApps(DateTime DateI, DateTime DateF);
        List<biz.bd2.Models.Checadas> GetReporteChecadas(DateTime DateI, DateTime DateF);
        List<biz.bd2.Models.SucursalesFront> GetSucursalesF();
        List<biz.bd2.Models.Vendedor> GetVentaVendedor(DateTime initDate, DateTime endDate);
        List<biz.bd2.Models.Filtro> GetVentaVendedorFiltro(DateTime initDate, DateTime endDate);
        public biz.bd2.Models.getArticulo GetStockArticulo(int sucursal, int codarticulo, string codAlmacen);
    }
}
