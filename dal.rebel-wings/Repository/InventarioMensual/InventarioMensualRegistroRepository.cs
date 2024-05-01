using biz.rebel_wings.Repository.InventarioMensual;
using dal.rebel_wings.DBContext;
using dal.rebel_wings.Repository.Generic;
using biz.rebel_wings.Models;
using Microsoft.EntityFrameworkCore;

namespace dal.rebel_wings.Repository.InventarioMensual
{ 
public class InventarioMensualRegistroRepository : GenericRepository<biz.rebel_wings.Entities.InventarioMensualRegistro>, IInventarioMensualRegistroRepository
{
    public InventarioMensualRegistroRepository(Db_Rebel_WingsContext context) : base(context)
    {
    }

        public biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro CreaRegistro(int city, int sucursal, string captura)
        {
            var timeNow = DateTime.Now;
            biz.rebel_wings.Entities.InventarioMensualRegistro _registro = new biz.rebel_wings.Entities.InventarioMensualRegistro();
            biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro __registro = new biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro();
            // FECHA DE INVENTARIOS

            _registro.City = city; 
            _registro.Sucursal = sucursal;
            _registro.Captura = captura;
            _registro.DateCaptura = timeNow;
            _registro.Procesado = false;

            _context.InventariosRegistrosMensuales.Add(_registro);

            __registro.City = city;
            __registro.Sucursal = sucursal;
            __registro.Captura = captura;
            __registro.DateCaptura = timeNow;
            __registro.Procesado = false;

            _context.SaveChanges();
            return __registro;
        }
        public biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro GetRegistro(int sucursal)
        {
            var timeNow = DateTime.Now;
            biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro __registro = new biz.rebel_wings.Models.InventarioMensual.InventarioMensualRegistro();
            var pendiente = _context.InventariosRegistrosMensuales.FirstOrDefault(x => x.Sucursal == sucursal);
            if (pendiente != null)
            {
                
                __registro.Procesado = pendiente.Procesado;
                __registro.City = pendiente.City;
                __registro.Sucursal = pendiente.Sucursal;
                __registro.Id = pendiente.Id;
                __registro.DateCaptura  = pendiente.DateCaptura;
                __registro.Captura = pendiente.Captura;

                return __registro;
            }
            else
            {
                __registro.Procesado = true;
                __registro.City = 0;
                __registro.Sucursal = 0;
                __registro.Id = 0;
                __registro.DateCaptura = timeNow;
                __registro.Captura = "null";

                return __registro;
            }
            
        }
        public Boolean procesadoRegistro(int registro)
        {
            
            try {
                var __registro = _context.InventariosRegistrosMensuales.FirstOrDefault(x => x.Id == registro);
                __registro.Procesado = true;

                _context.InventariosRegistrosMensuales.Update(__registro);
                _context.SaveChanges();

                return true; 
            } 
            catch { return false; }
        }

    }
}
