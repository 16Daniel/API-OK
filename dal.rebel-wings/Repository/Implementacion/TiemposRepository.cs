using biz.rebel_wings.Repository.Implementacion;
using dal.rebel_wings.DBContext;
using dal.rebel_wings.Repository.Generic;
using biz.rebel_wings.Models.Implementacion;
using Microsoft.EntityFrameworkCore;

namespace dal.rebel_wings.Repository.Implementacion
{

    public class TiemposRepository : GenericRepository<biz.rebel_wings.Entities.Tiempos>, ITiemposRepository
    {
        public TiemposRepository(Db_Rebel_WingsContext context) : base(context)
        {
        }

        public List<TiemposList> GetTiempos(string sucursal, DateTime initDate, DateTime endDate)
        {
            var TiemposList = _context.IT_TIEMPOs.Select(tiempos => new biz.rebel_wings.Models.Implementacion.TiemposList()
            {
                Id = tiempos.Id,
                IdComanda = tiempos.IdComanda,
                CodArticulo = tiempos.CodArticulo,
                Orden = tiempos.Orden,
                Posicion = tiempos.Posicion,
                Terminal = tiempos.Terminal,
                Hora = tiempos.Hora,
                Descripcion = tiempos.Descripcion,
                Unidades = tiempos.Unidades,
                Minutos = tiempos.Minutos,
                EnTiempo = tiempos.EnTiempo,
                Sucursal = tiempos.Sucursal
            }).Where(s => s.Hora.Date >= initDate.Date && s.Hora.Date <= endDate.Date && s.Sucursal == sucursal).OrderByDescending(s => s.Hora).ToList();
            return TiemposList.DistinctBy(x => x.IdComanda).ToList();
        }

        public List<RangosList> GetGraficaTiempos(string sucursal, DateTime initDate, DateTime endDate) {

            var TiemposList = _context.IT_TIEMPOs.Select(tiempos => new biz.rebel_wings.Models.Implementacion.TiemposList()
            {
                IdComanda = tiempos.IdComanda,
                CodArticulo = tiempos.CodArticulo,
                Orden = tiempos.Orden,
                Posicion = tiempos.Posicion,
                Terminal = tiempos.Terminal,
                Hora = tiempos.Hora,
                Descripcion = tiempos.Descripcion,
                Unidades = tiempos.Unidades,
                Minutos = tiempos.Minutos,
                EnTiempo = tiempos.EnTiempo,
                Sucursal = tiempos.Sucursal
            }).Where(s => s.Hora.Date >= initDate.Date && s.Hora.Date <= endDate.Date && s.Sucursal == sucursal).OrderByDescending(s => s.Hora).ToList();

            int R1=0, R2=0, R3=0;
            foreach (var i in TiemposList)
            {
                if (i.Minutos >= 0 && i.Minutos <= 15) { R1++; }
                if (i.Minutos >= 16 && i.Minutos <= 25) { R2++; }
                if (i.Minutos >= 26 && i.Minutos <= 30) { R3++; }
            }
            var multiG = new List<RangosList>();

            multiG.Add(new RangosList 
            {
             nomRango = "0-15 minutos", RangoValor = R1
            });
            multiG.Add(new RangosList
            {
                nomRango = "16-25 minutos",
                RangoValor = R2
            });
            multiG.Add(new RangosList
            {
                nomRango = "26-30 minutos",
                RangoValor = R3
            });


            return multiG;
        }
    }
}