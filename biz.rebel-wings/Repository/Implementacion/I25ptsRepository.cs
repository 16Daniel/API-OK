using biz.rebel_wings.Repository.Generic;

namespace biz.rebel_wings.Repository.Implementacion
{

    public interface I25ptsRepository : IGenericRepository<Entities._25pts>
    {
        List<biz.rebel_wings.Models.Implementacion._25ptsList> Get25pts(string branch, DateTime initDate, DateTime endDate);
        List<biz.rebel_wings.Models.Implementacion.sucAudita> GetGrafica25pts(string branch, DateTime initDate, DateTime endDate);
    }
}