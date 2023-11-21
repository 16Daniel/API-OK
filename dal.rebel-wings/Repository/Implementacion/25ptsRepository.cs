using biz.rebel_wings.Repository.Implementacion;
using dal.rebel_wings.DBContext;
using dal.rebel_wings.Repository.Generic;
using biz.rebel_wings.Models.Implementacion;
using Microsoft.EntityFrameworkCore;

namespace dal.rebel_wings.Repository.Implementacion
{

    public class _25ptsRepository : GenericRepository<biz.rebel_wings.Entities._25pts>, I25ptsRepository
    {
        public _25ptsRepository(Db_Rebel_WingsContext context) : base(context)
        {
        }

        public List<_25ptsList> Get25pts(string branch, DateTime initDate, DateTime endDate)
        {
            var _25List = _context.IT_25PTs.Select(_25pts => new biz.rebel_wings.Models.Implementacion._25ptsList()
            {
                Id = _25pts.Id,
                FechaIni = _25pts.FechaIni,
                Sala = _25pts.Sala,
                Mesa = _25pts.Mesa,
                TotalAyc = _25pts.TotalAyc,
                Cobros = _25pts.Cobros,
                CobrosMinimos = _25pts.CobrosMinimos,
                Diferencia = _25pts.Diferencia,
                Justificacion = _25pts.Justificacion,
                Usuario = _25pts.Usuario,
                Sucursal = _25pts.Sucursal
            }).Where(s=>s.FechaIni.Date >= initDate.Date && s.FechaIni.Date <= endDate.Date && s.Sucursal == branch).OrderByDescending(s => s.FechaIni).ToList();
            return _25List;
        }
        public List<sucAudita> GetGrafica25pts(string branch, DateTime initDate, DateTime endDate)
        {
            var _25List = _context.IT_25PTs.Select(_25pts => new biz.rebel_wings.Models.Implementacion._25ptsList()
            {
                FechaIni = _25pts.FechaIni,
                Sala = _25pts.Sala,
                Mesa = _25pts.Mesa,
                TotalAyc = _25pts.TotalAyc,
                Cobros = _25pts.Cobros,
                CobrosMinimos = _25pts.CobrosMinimos,
                Diferencia = _25pts.Diferencia,
                Justificacion = _25pts.Justificacion,
                Usuario = _25pts.Usuario,
                Sucursal = _25pts.Sucursal
            }).Where(s => s.FechaIni.Date >= initDate.Date && s.FechaIni.Date <= endDate.Date && s.Sucursal == branch).OrderByDescending(s => s.FechaIni).ToList();

            
            var multiG = new List<sucAudita>();
            multiG.Add(new sucAudita
            {
                incidencias = _25List.Count
            });

            return multiG;
        }
    }
}