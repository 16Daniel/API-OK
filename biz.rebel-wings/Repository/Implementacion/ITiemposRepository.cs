using biz.rebel_wings.Repository.Generic;

namespace biz.rebel_wings.Repository.Implementacion
{

    public interface ITiemposRepository : IGenericRepository<Entities.Tiempos>
    {
        List<biz.rebel_wings.Models.Implementacion.TiemposList> GetTiempos(string sucursal, DateTime initDate, DateTime endDate);
        List<biz.rebel_wings.Models.Implementacion.RangosList> GetGraficaTiempos(string sucursal, DateTime initDate, DateTime endDate);
    }
}
