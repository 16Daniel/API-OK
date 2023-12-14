using biz.bd2.Repository.Stock;
using dal.bd2.DBContext;
using dal.bd2.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.bd2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace dal.bd2.Repository.Stock
{
    public class StockRepository : GenericRepository<biz.bd2.Entities.Stock>, IStockRepository
    {
        public StockRepository(BD2Context context) : base(context)
        {

        }
        public List<biz.bd2.Models.StockDto> GetStock(int id_sucursal)
        {
            List<biz.bd2.Models.StockDto> _stock = new List<biz.bd2.Models.StockDto>();
            List<biz.bd2.Models.StockDto> _stock2 = new List<biz.bd2.Models.StockDto>();
            var timeNow = DateTime.Now;
            var serie = _context.RemCajasfronts.FirstOrDefault(x => x.Idfront == id_sucursal).Codalmventas;
                if (serie != null)
                {
                    _stock = _context.Stocks
                        .Join(_context.Articuloscamposlibres,
                        art => art.Codarticulo,
                        stk => stk.Codarticulo,
                        (art, stk) => new biz.bd2.Models.StockDto()
                        {
                          Codalmacen = art.Codalmacen,
                          Codarticulo = stk.Codarticulo,
                          Regulariza = stk.Regulariza,
                          Unidadessat = stk.Unidadessat,
                          Unidadmedida = stk.UnidadMedida,
                        })
                        .Join(_context.Articulos1,
                        art => art.Codarticulo,
                        stk => stk.Codarticulo,
                        (art, stk) => new biz.bd2.Models.StockDto()
                        {
                          Codalmacen = art.Codalmacen,
                          Descripcion = stk.Descripcion,
                          Codarticulo = art.Codarticulo,
                          Regulariza = art.Regulariza,
                          Unidadessat = art.Unidadessat,
                          Unidadmedida = art.Unidadmedida,
                        })
                        .Where(s => s.Codalmacen == serie && s.Regulariza == "T").ToList();

                }
                if (timeNow.Hour < 3) {

                    _stock = _stock.Where(s => !_context.Moviments.Where(es => es.Fecha == DateTime.Now.Date.AddDays(-1) && es.Codarticulo == s.Codarticulo && es.Codalmacenorigen == s.Codalmacen && es.Codalmacendestino == "" && es.Hora.Value.Hour > 3 && es.Tipo == "REG").Any()).ToList();
                    _stock2 = _stock.Where(s => !_context.Moviments.Where(es => es.Fecha == DateTime.Now.Date && es.Codarticulo == s.Codarticulo && es.Codalmacenorigen == s.Codalmacen && es.Codalmacendestino == "" && es.Tipo == "REG").Any()).ToList();
                    if(_stock.LongCount() > 0){

                        if(_stock2.LongCount() > 0) {

                             return _stock;

                        }
                        else{
                             return _stock2;

                        }
                    
                    }
                    else {

                             return _stock;
           
                    }
                }
                else {

                   _stock = _stock.Where(s => !_context.Moviments.Where(es => es.Fecha == DateTime.Now.Date && es.Codarticulo == s.Codarticulo && es.Codalmacenorigen == s.Codalmacen && es.Codalmacendestino == "" && es.Hora.Value.Hour >= 7 && es.Hora.Value.Hour < 17 && es.Tipo == "REG").Any()).ToList();
                   return _stock;
                }

             

             
        }

        public List<biz.bd2.Models.StockDto> GetStockV(int id_sucursal)
        {
            List<biz.bd2.Models.StockDto> _stock = new List<biz.bd2.Models.StockDto>();
            List<biz.bd2.Models.StockDto> _stock2 = new List<biz.bd2.Models.StockDto>();
            var Hrs = DateTime.Now.Hour;
            var ampm = Hrs >= 12 ? "PM" : "AM";

            var serie = _context.RemCajasfronts.FirstOrDefault(x => x.Idfront == id_sucursal).Codalmventas;
            if (serie != null)
            {
                _stock = _context.Stocks
                    .Join(_context.Articuloscamposlibres,
                    art => art.Codarticulo,
                    stk => stk.Codarticulo,
                    (art, stk) => new biz.bd2.Models.StockDto()
                    {
                        Codalmacen = art.Codalmacen,
                        Codarticulo = stk.Codarticulo,
                        Regulariza = stk.Regulariza,
                        Unidadessat = stk.Unidadessat,
                        Unidadmedida = stk.UnidadMedida,
                        RegularizaSemanal = stk.RegularizaSemanal,
                    })
                    .Join(_context.Articulos1,
                    art => art.Codarticulo,
                    stk => stk.Codarticulo,
                    (art, stk) => new biz.bd2.Models.StockDto()
                    {
                        Codalmacen = art.Codalmacen,
                        Descripcion = stk.Descripcion,
                        Codarticulo = art.Codarticulo,
                        Regulariza = art.Regulariza,
                        Unidadessat = art.Unidadessat,
                        Unidadmedida = stk.Unidadmedida,
                        RegularizaSemanal = art.RegularizaSemanal,
                    })
                    .Where(s => s.Codalmacen == serie && s.RegularizaSemanal == "T").ToList();

            }
            if (ampm.ToString().Equals("AM"))
            {

                _stock = _stock.Where(s => !_context.Moviments.Where(es => es.Fecha == DateTime.Now.Date && es.Codarticulo == s.Codarticulo && es.Codalmacenorigen == s.Codalmacen && es.Codalmacendestino == "" && es.Hora.Value.Hour > 1 && es.Hora.Value.Hour < 7 && es.Tipo == "REG").Any()).ToList();
                

                    return _stock;

                
            }
            else
            {

                _stock = _stock.Where(s => !_context.Moviments.Where(es => es.Fecha == DateTime.Now.Date.AddDays(1) && es.Codarticulo == s.Codarticulo && es.Codalmacenorigen == s.Codalmacen && es.Codalmacendestino == "" && es.Hora.Value.Hour > 1 && es.Hora.Value.Hour < 7 && es.Tipo == "REG").Any()).ToList();
                return _stock;
            }




        }

        public decimal StockValidate(int id_sucursal, int codarticulo)
        {
            decimal _stock = 0;
            var serie = _context.RemCajasfronts.FirstOrDefault(x => x.Idfront == id_sucursal).Codalmventas;
            if (serie != null)
            {
                _stock = (decimal)_context.Stocks
                    .Join(_context.Articuloscamposlibres,
                    art => art.Codarticulo,
                    stk => stk.Codarticulo,
                    (art, stk) => new
                    {
                      Codalmacen = art.Codalmacen,
                      Codarticulo = stk.Codarticulo,
                      Regulariza = stk.Regulariza,
                      Unidadessat = stk.Unidadessat,
                      Unidadmedida = stk.UnidadMedida,
                      art.Stock1

                    })
                    .Join(_context.Articulos1,
                    art => art.Codarticulo,
                    stk => stk.Codarticulo,
                    (art, stk) => new
                    {
                      Codalmacen = art.Codalmacen,
                      Descripcion = stk.Descripcion,
                      Codarticulo = art.Codarticulo,
                      Regulariza = art.Regulariza,
                      Unidadessat = art.Unidadessat,
                      Unidadmedida = art.Unidadmedida,
                      art.Stock1
                    })
                    .SingleOrDefault(s => s.Codalmacen == serie && s.Codarticulo == codarticulo && s.Regulariza == "T").Stock1.Value;

            }

            return _stock;
        }


        public decimal StockValidateV(int id_sucursal, int codarticulo)
        {
            decimal _stock = 0;
            var serie = _context.RemCajasfronts.FirstOrDefault(x => x.Idfront == id_sucursal).Codalmventas;
            if (serie != null)
            {
                _stock = (decimal)_context.Stocks
                    .Join(_context.Articuloscamposlibres,
                    art => art.Codarticulo,
                    stk => stk.Codarticulo,
                    (art, stk) => new
                    {
                        Codalmacen = art.Codalmacen,
                        Codarticulo = stk.Codarticulo,
                        RegularizaSemanal = stk.RegularizaSemanal,
                        Unidadessat = stk.Unidadessat,
                        Unidadmedida = stk.UnidadMedida,
                        art.Stock1

                    })
                    .Join(_context.Articulos1,
                    art => art.Codarticulo,
                    stk => stk.Codarticulo,
                    (art, stk) => new
                    {
                        Codalmacen = art.Codalmacen,
                        Descripcion = stk.Descripcion,
                        Codarticulo = art.Codarticulo,
                        RegularizaSemanal = art.RegularizaSemanal,
                        Unidadessat = art.Unidadessat,
                        Unidadmedida = art.Unidadmedida,
                        art.Stock1
                    })
                    .SingleOrDefault(s => s.Codalmacen == serie && s.Codarticulo == codarticulo && s.RegularizaSemanal == "T").Stock1.Value;

            }

            return _stock;
        }


        public biz.bd2.Models.StockDto UpdateStock(int codArticulo, string codAlmacen, double cantidad)
        {
            biz.bd2.Models.StockDto _stock = new biz.bd2.Models.StockDto();

            //FECHA DE INVENTARIOS
            var tablaInv = DateTime.Now.Date.AddDays(-1);
            if (_context.Inventarios.FirstOrDefault(x => x.Codalmacen == codAlmacen && x.Fecha == DateTime.Now.Date) != null) {
              tablaInv  = _context.Inventarios.FirstOrDefault(x => x.Codalmacen == codAlmacen && x.Fecha == DateTime.Now.Date).Fecha;
            }
            else { tablaInv = DateTime.Now.Date.AddDays(-1); }

            var __stock = _context.Stocks.FirstOrDefault(x => x.Codarticulo == codArticulo && x.Codalmacen == codAlmacen);
            double _stockAnterior = __stock.Stock1.Value;
            if (__stock != null)
            {
                __stock.Stock1 = cantidad;

                _stock.Codalmacen = codAlmacen;
                _stock.Descripcion = _stock.Descripcion;
                _stock.Codarticulo = codArticulo;
                _stock.Regulariza = "T";
                _stock.Unidadessat = "";
                _stock.Stock1 = cantidad;

                _context.Stocks.Update(__stock);
              if (tablaInv != DateTime.Now.Date) {
                biz.bd2.Entities.Inventario _inventario = new biz.bd2.Entities.Inventario();
                _inventario.Fecha = DateTime.Now.Date;
                _inventario.Codalmacen = codAlmacen;
                _inventario.Tipovaloracion = -3;
                _inventario.Serie = "";
                _inventario.Numero = 0;
                _inventario.Codvendedor = -1;
                _inventario.Completo = "F";
                _inventario.Metodo = 1;
                _inventario.Inicial = "F";
                _inventario.Bloqueado = "F";
                _inventario.Tipovaloraciondmn = null;
                _inventario.Estado = 0;
                _inventario.Escierre = false;
                _inventario.EnlaceEjercicio = null;
                _inventario.EnlaceEmpresa = null;
                _inventario.EnlaceUsuario = null;
                _inventario.EnlaceAsiento = null;

                _context.Inventarios.Add(_inventario);

              }

                biz.bd2.Entities.Moviment _moviment = new biz.bd2.Entities.Moviment();
                _moviment.Codalmacenorigen = codAlmacen;
                _moviment.Codalmacendestino = "";
                _moviment.Numserie = "";
                _moviment.Codarticulo = codArticulo;
                _moviment.Talla = ".";
                _moviment.Color = ".";
                _moviment.Precio = _context.Articuloscamposlibres.FirstOrDefault(x => x.Codarticulo == codArticulo)?.Precioproveedor;
                _moviment.Fecha = DateTime.Now.Date;
                _moviment.Hora = Convert.ToDateTime("1899-12-30 " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ".000");
                _moviment.Codprocli = 0;
                _moviment.Tipo = "REG";
                _moviment.Unidades = cantidad;
                _moviment.Seriedoc = "";
                _moviment.Numdoc = 0;
                _moviment.Seriecompra = "";
                _moviment.Numfaccompra = -1;
                _moviment.Caja = "";
                _moviment.Stock = _stockAnterior;
                _moviment.Pvp = 0;
                _moviment.Codmonedapvp = 1;
                _moviment.Calcmovpost = "F";
                _moviment.Udmedida2 = 0;
                _moviment.Zona = "";
                _moviment.Pvpdmn = null;
                _moviment.Preciodmn = null;
                _moviment.Stock2 = 0;

                _context.Moviments.Add(_moviment);

                _context.SaveChanges();
                return _stock;
            }
            else
            {
                return _stock;
            }
        }

        public biz.bd2.Models.StockDto UpdateStockV(int codArticulo, string codAlmacen, double cantidad)
        {
            biz.bd2.Models.StockDto _stock = new biz.bd2.Models.StockDto();
            // FECHA DE INVENTARIOS
            var tablaInv = DateTime.Now.Date;
            var Hrs = DateTime.Now.Hour;
            var ampm = Hrs >= 12 ? "PM" : "AM";
            var diAsignado = DateTime.Now.Date;

            if (ampm.ToString().Equals("PM"))
            {
                Console.WriteLine("SI ES PM");
                tablaInv = DateTime.Now.Date.AddDays(1);
                diAsignado = DateTime.Now.Date.AddDays(1);
            }
            else
            {
                Console.WriteLine("SI ES AM ");
                tablaInv = DateTime.Now.Date;
                diAsignado = DateTime.Now.Date;
            }

            if (_context.Inventarios.FirstOrDefault(x => x.Codalmacen == codAlmacen && x.Fecha == diAsignado.Date) != null)
            {
                tablaInv = _context.Inventarios.FirstOrDefault(x => x.Codalmacen == codAlmacen && x.Fecha == diAsignado.Date).Fecha;
            }
            else {
                if (ampm.ToString().Equals("PM"))
                {
                    tablaInv = DateTime.Now.Date.AddDays(1); ;
                }
                
            }

            var __stock = _context.Stocks.FirstOrDefault(x => x.Codarticulo == codArticulo && x.Codalmacen == codAlmacen);
            double _stockAnterior = __stock.Stock1.Value;
            if (__stock != null)
            {
                __stock.Stock1 = cantidad;

                _stock.Codalmacen = codAlmacen;
                _stock.Descripcion = _stock.Descripcion;
                _stock.Codarticulo = codArticulo;
                _stock.Regulariza = "T";
                _stock.Unidadessat = "";
                _stock.Stock1 = cantidad;

                _context.Stocks.Update(__stock);

                if (tablaInv != diAsignado)
                {
                    var FechV = diAsignado;

                    biz.bd2.Entities.Inventario _inventario = new biz.bd2.Entities.Inventario();

                    _inventario.Fecha = FechV.Date;
                    _inventario.Codalmacen = codAlmacen;
                    _inventario.Tipovaloracion = -3;
                    _inventario.Serie = "";
                    _inventario.Numero = 0;
                    _inventario.Codvendedor = -1;
                    _inventario.Completo = "F";
                    _inventario.Metodo = 1;
                    _inventario.Inicial = "F";
                    _inventario.Bloqueado = "F";
                    _inventario.Tipovaloraciondmn = null;
                    _inventario.Estado = 0;
                    _inventario.Escierre = false;
                    _inventario.EnlaceEjercicio = null;
                    _inventario.EnlaceEmpresa = null;
                    _inventario.EnlaceUsuario = null;
                    _inventario.EnlaceAsiento = null;

                    _context.Inventarios.Add(_inventario);

                }

                DateTime FechVC = diAsignado.Date.AddHours(2);

                biz.bd2.Entities.Moviment _moviment = new biz.bd2.Entities.Moviment();
                _moviment.Codalmacenorigen = codAlmacen;
                _moviment.Codalmacendestino = "";
                _moviment.Numserie = "";
                _moviment.Codarticulo = codArticulo;
                _moviment.Talla = ".";
                _moviment.Color = ".";
                _moviment.Precio = _context.Articuloscamposlibres.FirstOrDefault(x => x.Codarticulo == codArticulo)?.Precioproveedor;
                _moviment.Fecha = FechVC.Date;
                _moviment.Hora = Convert.ToDateTime("1899-12-30 " + FechVC.Hour + ":" + FechVC.Minute + ":" + FechVC.Second + ".000");
                _moviment.Codprocli = 0;
                _moviment.Tipo = "REG";
                _moviment.Unidades = cantidad;
                _moviment.Seriedoc = "";
                _moviment.Numdoc = 0;
                _moviment.Seriecompra = "";
                _moviment.Numfaccompra = -1;
                _moviment.Caja = "";
                _moviment.Stock = _stockAnterior;
                _moviment.Pvp = 0;
                _moviment.Codmonedapvp = 1;
                _moviment.Calcmovpost = "F";
                _moviment.Udmedida2 = 0;
                _moviment.Zona = "";
                _moviment.Pvpdmn = null;
                _moviment.Preciodmn = null;
                _moviment.Stock2 = 0;

                _context.Moviments.Add(_moviment);


                _context.SaveChanges();
                return _stock;
            }
            else
            {
                return _stock;
            }
        }

        public List<Mermas> GetMermas(int branch, DateTime initDate, DateTime endDate)
        {
            var mermas =
                from moviment in _context.Moviments
                join articulo1 in _context.Articulos1 on moviment.Codarticulo equals articulo1.Codarticulo
                join rem in _context.RemCajasfronts on new
                {
                    Codalmmermas = EF.Functions.Collate(moviment.Codalmacendestino, "Modern_Spanish_CI_AS"),
                    Ventas = EF.Functions.Collate(moviment.Codalmacenorigen, "Modern_Spanish_CI_AS")
                } equals new
                {
                    Codalmmermas = rem.Codalmmermas,
                    Ventas = rem.Codalmventas
                }
                join remFront in _context.RemFronts on rem.Idfront equals remFront.Idfront
                where (moviment.Fecha >= initDate && moviment.Fecha <= endDate) && rem.Idfront == branch
                orderby moviment.Fecha descending 
                select new Mermas()
                {
                    Description = articulo1.Descripcion,
                    Price = moviment.Precio.Value,
                    Unity = moviment.Unidades.Value,
                    UnitMeasure = articulo1.Unidadmedida,
                    Fecha = (DateTime)moviment.Fecha
                };
            return mermas.ToList();
        }

        public List<Reporte> GetReporte(DateTime Date)
        {
            List<Reporte> reportes = new List<Reporte>();
            SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SPS_MAT_REPORTE";
            cmd.Parameters.Add("@FECHA", System.Data.SqlDbType.VarChar, 10).Value = Date.ToString("dd/MM/yyyy");
            cmd.CommandTimeout = 120;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Reporte repp = new Reporte();
                repp.cod = (string)reader["COD"];
                repp.Sucursal = (string)reader["SUCURSAL"];
                repp.Articulo = (string)reader["ARTICULO"];
                repp.Seccion = (string)reader["SECCION"];
                repp.InvAyer = (string)reader["INVAYER"];
                repp.ConsumoAyer = (double)reader["CONSUMOAYER"];
                repp.TraspasoAyer = (double)reader["TRASPASOAYER"];
                repp.InvHoy = (string)reader["INVHOY"];
                repp.Captura = (DateTime)reader["CAPTURA"];
                repp.InvFormula = (double)reader["INVFORMULA"];
                repp.Diferencia = (double)reader["DIFERENCIA"];
                reportes.Add(repp);
            }
            connection.Close();

            //var basesuc =
            //    from almacen in _context.Almacens
            //    orderby almacen.Codalmacen ascending
            //    where (almacen.Notas.Contains("RW"))
            //    select new Reporte()
            //    {
            //        cod = almacen.Codalmacen,
            //        Sucursal = almacen.Nombrealmacen,

            //    };

            //var artreg =
            //    from art in _context.Articulos1
            //    join camp in _context.Articuloscamposlibres on art.Codarticulo equals camp.Codarticulo
            //    orderby art.Codarticulo ascending
            //    where (camp.Regulariza == "T")
            //    select new Reporte() { Articulo = art.Descripcion };

            //var CrossJoinResult =
            //    from sucursales in basesuc
            //    from articulos in artreg
            //    select new Reporte()
            //    {
            //        cod = sucursales.cod,
            //        Sucursal = sucursales.Sucursal,
            //        Articulo = articulos.Articulo,
            //        InvAyer = "0",
            //        InvHoy = "0",
            //        ConsumoAyer = 0,
            //        TraspasoAyer = 0,
            //        InvFormula = 0,
            //        Captura = DateTime.Now.Date,

            //    };

            //var inv =
            //    from moviment in _context.Moviments
            //    join articulo1 in _context.Articulos1 on moviment.Codarticulo equals articulo1.Codarticulo
            //    join campos in _context.Articuloscamposlibres on articulo1.Codarticulo equals campos.Codarticulo
            //    join almac in _context.Almacens on moviment.Codalmacenorigen equals almac.Codalmacen
            //    where (moviment.Fecha.Value.Date == Date.Date) && (moviment.Hora.Value.Hour >= 7 && moviment.Hora.Value.Hour <= 17)
            //    where (campos.Regulariza == "T") && (moviment.Tipo == "REG")
            //    orderby articulo1.Descripcion ascending, moviment.Codalmacenorigen ascending
            //    select new Reporte()
            //    {
            //        cod = moviment.Codalmacenorigen,
            //        Sucursal = almac.Nombrealmacen,
            //        Articulo = articulo1.Descripcion,
            //        InvHoy = moviment.Unidades.Value.ToString(),
            //        Captura = moviment.Hora.Value,

            //        //Description = articulo1.Descripcion,
            //        //Price = moviment.Precio.Value,
            //        //Unity = moviment.Unidades.Value,
            //        //UnitMeasure = articulo1.Unidadmedida
            //    };


            //var invAyer =
            //    from moviment in _context.Moviments
            //    join articulo1 in _context.Articulos1 on moviment.Codarticulo equals articulo1.Codarticulo
            //    join campos in _context.Articuloscamposlibres on articulo1.Codarticulo equals campos.Codarticulo
            //    join almac in _context.Almacens on moviment.Codalmacenorigen equals almac.Codalmacen
            //    where (moviment.Fecha.Value.Date == Date.AddDays(-1).Date) && (moviment.Hora.Value.Hour >= 7 && moviment.Hora.Value.Hour <= 17)
            //    where (campos.Regulariza == "T") && (moviment.Tipo == "REG")
            //    orderby articulo1.Descripcion ascending, moviment.Codalmacenorigen ascending
            //    select new Reporte()
            //    {

            //        cod = moviment.Codalmacenorigen,
            //        Sucursal = almac.Nombrealmacen,
            //        Articulo = articulo1.Descripcion,
            //        InvAyer = moviment.Unidades.Value.ToString(),

            //        //Description = articulo1.Descripcion,
            //        //Price = moviment.Precio.Value,
            //        //Unity = moviment.Unidades.Value,
            //        //UnitMeasure = articulo1.Unidadmedida
            //    };



            //var consumo =
            //    from albvt in _context.Albventaconsumos
            //    join albvtcab in _context.Albventacabs on new
            //    {
            //        albvt.Numserie,
            //        albvt.Numalbaran,
            //        albvt.N
            //    }
            //    equals new
            //    {
            //        albvtcab.Numserie,
            //        albvtcab.Numalbaran,
            //        albvtcab.N
            //    }
            //    join art in _context.Articulos1 on albvt.Codarticulo equals art.Codarticulo
            //    join almac in _context.Almacens on albvt.Codalmacen equals almac.Codalmacen
            //    join artcamp in _context.Articuloscamposlibres on albvt.Codarticulo equals artcamp.Codarticulo
            //    where (albvtcab.Fecha.Value.Date == Date.AddDays(-1).Date) && (artcamp.Regulariza == "T")
            //    group new { albvt, albvtcab, almac, art, artcamp } by new { almac.Nombrealmacen, art.Descripcion } into x
            //    select new Reporte()
            //    {

            //        Sucursal = x.Key.Nombrealmacen,
            //        Articulo = x.Key.Descripcion,
            //        ConsumoAyer = x.Sum(cn => cn.albvt.Consumo.Value),

            //        //Description = articulo1.Descripcion,
            //        //Price = moviment.Precio.Value,
            //        //Unity = moviment.Unidades.Value,
            //        //UnitMeasure = articulo1.Unidadmedida
            //    };
            //var compras =
            //    from albcomp in _context.Albcompralins
            //    join almac in _context.Almacens on albcomp.Codalmacen equals almac.Codalmacen
            //    join art in _context.Articulos1 on albcomp.Codarticulo equals art.Codarticulo
            //    join albcompcab in _context.Albcompracabs on new
            //    {
            //        albcomp.Numserie,
            //        albcomp.Numalbaran,
            //        albcomp.N
            //    }
            //    equals new
            //    {
            //        albcompcab.Numserie,
            //        albcompcab.Numalbaran,
            //        albcompcab.N
            //    }
            //    join artcamp in _context.Articuloscamposlibres on art.Codarticulo equals artcamp.Codarticulo
            //    where (albcompcab.Fechaalbaran.Value.Date == Date.AddDays(-1).Date) && (artcamp.Regulariza == "T")
            //    group new { albcomp, albcompcab, almac, art, artcamp } by new { almac.Nombrealmacen, art.Descripcion } into x
            //    select new Reporte()
            //    {
            //        Sucursal = x.Key.Nombrealmacen,
            //        Articulo = x.Key.Descripcion,
            //        TraspasoAyer = x.Sum(cn => cn.albcomp.Unidadestotal.Value),

            //    };

            ////var list = from ayer in invAyer
            ////           join hoy in inv on new { ayer.Sucursal, ayer.Articulo } equals new { hoy.Sucursal, hoy.Articulo } into inv
            ////           join cons in consumo on new { ayer.Sucursal, ayer.Articulo } equals new { cons.Sucursal, cons.Articulo }
            ////           join comp in compras on new { ayer.Sucursal, ayer.Articulo } equals new { comp.Sucursal, comp.Articulo }
            ////           orderby ayer.Sucursal, ayer.Articulo ascending
            ////           select new Reporte() 
            ////           { 
            ////            Sucursal = ayer.Sucursal,
            ////            Articulo = ayer.Articulo,
            ////            InvAyer = ayer.InvAyer,
            ////            InvHoy = hoy.InvHoy,
            ////            ConsumoAyer = cons.ConsumoAyer,
            ////            TraspasoAyer = comp.TraspasoAyer,
            ////            InvFormula = ((ayer.InvAyer + comp.TraspasoAyer) - cons.ConsumoAyer),
            ////            Diferencia = (hoy.InvHoy) - ((ayer.InvAyer + comp.TraspasoAyer) - cons.ConsumoAyer),

            ////           };

            //var list0 = from ayer in CrossJoinResult
            //            join hoy in invAyer.Distinct() on new { ayer.Sucursal, ayer.Articulo } equals new { hoy.Sucursal, hoy.Articulo } into inventarios
            //            from ed in inventarios.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = ayer.cod,
            //                Sucursal = ayer.Sucursal,
            //                Articulo = ayer.Articulo,
            //                InvAyer = ed.InvAyer == null ? "SIN CAPTURA" : ed.InvAyer,


            //            };

            //var list1 = from ayer in list0
            //            join hoy in inv on new { ayer.Sucursal, ayer.Articulo } equals new { hoy.Sucursal, hoy.Articulo } into inventarios
            //            from ed in inventarios.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = ayer.cod,
            //                Sucursal = ayer.Sucursal,
            //                Articulo = ayer.Articulo,
            //                InvAyer = ayer.InvAyer,
            //                InvHoy = ed.InvHoy == null ? "SIN CAPTURA" : ed.InvHoy,
            //                Captura = ed.Captura == null ? DateTime.Now.Date : ed.Captura,

            //            };
            //var list2 = from x in list1
            //            join z in consumo on new { x.Sucursal, x.Articulo } equals new { z.Sucursal, z.Articulo } into xz
            //            from ed in xz.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = x.cod,
            //                Sucursal = x.Sucursal,
            //                Articulo = x.Articulo,
            //                InvAyer = x.InvAyer,
            //                InvHoy = x.InvHoy,
            //                ConsumoAyer = ed.ConsumoAyer == null ? 0 : ed.ConsumoAyer,
            //                Captura = x.Captura,

            //            };
            //var list3 = from x in list2
            //            join z in compras on new { x.Sucursal, x.Articulo } equals new { z.Sucursal, z.Articulo } into xz
            //            from ed in xz.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = x.cod,
            //                Sucursal = x.Sucursal,
            //                Articulo = x.Articulo,
            //                InvAyer = x.InvAyer,
            //                InvHoy = x.InvHoy,
            //                ConsumoAyer = x.ConsumoAyer,
            //                TraspasoAyer = ed.TraspasoAyer == null ? 0 : ed.TraspasoAyer,
            //                InvFormula = (double.Parse((x.InvAyer == "SIN CAPTURA" ? "0" : x.InvAyer)) + (ed.TraspasoAyer == null ? 0 : ed.TraspasoAyer)) - (x.ConsumoAyer),
            //                Diferencia = ((double.Parse(x.InvHoy == "SIN CAPTURA" ? "0" : x.InvHoy)) - ((double.Parse((x.InvAyer == "SIN CAPTURA" ? "0" : x.InvAyer)) + (ed.TraspasoAyer == null ? 0 : ed.TraspasoAyer)) - (x.ConsumoAyer))),
            //                Captura = x.Captura,
            //            };


            ////return inv.Distinct().ToList();
            ////return list3.Distinct().OrderBy(x => x.Articulo).ThenBy(x => x.cod).ToList();
            //var list4 = list3.GroupBy(x => new { x.Sucursal, x.Articulo }).Select(x => x.First()).ToList();
            //return list4.OrderBy(x => x.Articulo).ThenBy(x => x.cod).ToList(); 
            return reportes;
        }
        public List<Vendedor> GetVentaVendedor(DateTime initDate, DateTime endDate)
        {
            var venta =
                //from moviment in _context.Moviments
                //join articulo1 in _context.Articulos1 on moviment.Codarticulo equals articulo1.Codarticulo
                //join rem in _context.RemCajasfronts on new
                //{
                //    Codalmmermas = EF.Functions.Collate(moviment.Codalmacendestino, "Modern_Spanish_CI_AS"),
                //    Ventas = EF.Functions.Collate(moviment.Codalmacenorigen, "Modern_Spanish_CI_AS")
                //} equals new
                //{
                //    Codalmmermas = rem.Codalmmermas,
                //    Ventas = rem.Codalmventas
                //}
                //join remFront in _context.RemFronts on rem.Idfront equals remFront.Idfront
                //where (moviment.Fecha = initDate ) 
                //orderby moviment.Fecha descending
                //select new Mermas()
                //{
                //    Description = articulo1.Descripcion,
                //    Price = moviment.Precio.Value,
                //    Unity = moviment.Unidades.Value,
                //    UnitMeasure = articulo1.Unidadmedida
                //};
                from ventalin in _context.Albventalins
                join art in _context.Articulos1 on ventalin.Codarticulo equals art.Codarticulo
                join ventacab in _context.Albventacabs on new
                {
                    ventalin.N,
                    ventalin.Numalbaran,
                    ventalin.Numserie
                } equals new
                {
                    ventacab.N,
                    ventacab.Numalbaran,
                    ventacab.Numserie
                }
                join almacen in _context.Almacens on ventalin.Codalmacen equals almacen.Codalmacen
                join rem in _context.RemCajasfronts on new
                {
                    Ventas = EF.Functions.Collate(almacen.Codalmacen, "Modern_Spanish_CI_AS")
                } equals new
                {
                    Ventas = rem.Codalmventas
                }
                join remFront in _context.RemFronts on rem.Idfront equals remFront.Idfront
                join vendedor in _context.Vendedores on ventalin.Codvendedor equals vendedor.Codvendedor
                join campArt in _context.Articuloscamposlibres on ventalin.Codarticulo equals campArt.Codarticulo
                where ventacab.Fecha.Value.Date >= initDate.Date && ventacab.Fecha <= endDate.Date && remFront.Titulo.ToUpper().Contains("RW") && !ventacab.Numserie.Contains("%I") && ventalin.Tipo.Contains("V") && ventalin.Lineaoculta.Contains("F")
                group new { ventalin, art, ventacab, almacen, rem, remFront, vendedor, campArt } by new { remFront.Titulo, vendedor.Nomvendedor, art.Descripcion, ventalin.Preciodefecto, campArt.Cerveza, art.Dpto, campArt.Filtro } into x
                orderby x.Key.Titulo ascending, x.Key.Nomvendedor ascending
                select new Vendedor()
                {
                    Sucursal = x.Key.Titulo,
                    Vendedores = x.Key.Nomvendedor,
                    Articulo = x.Key.Descripcion,
                    Uds = x.Sum(cn => cn.ventalin.Unidadestotal.Value),
                    Precio = x.Key.Preciodefecto.Value * x.Sum(cn => cn.ventalin.Unidadestotal.Value),
                    Cerveza = x.Key.Cerveza,
                    CodDpto = x.Key.Dpto,
                    Filtro = x.Key.Filtro,

                };

            var venta2 = from x in _context.Departamentos
                         join z in venta on x.Numdpto.ToString() equals z.CodDpto.ToString()
                         orderby z.Sucursal ascending, z.Vendedores ascending, z.Articulo ascending
                         select new Vendedor()
                         {
                             Sucursal = z.Sucursal,
                             Vendedores = z.Vendedores,
                             Articulo = z.Articulo,
                             Uds = z.Uds,
                             Precio = z.Precio,
                             Cerveza = z.Cerveza,
                             CodDpto = z.CodDpto,
                             Departamento = x.Descripcion,
                             Filtro = z.Filtro
                         };

            return venta2.ToList();
        }
        public List<Filtro> GetVentaVendedorFiltro(DateTime initDate, DateTime endDate)
        {
            var venta =

                from ventalin in _context.Albventalins
                join art in _context.Articulos1 on ventalin.Codarticulo equals art.Codarticulo
                join ventacab in _context.Albventacabs on new
                {
                    ventalin.N,
                    ventalin.Numalbaran,
                    ventalin.Numserie
                } equals new
                {
                    ventacab.N,
                    ventacab.Numalbaran,
                    ventacab.Numserie
                }
                join almacen in _context.Almacens on ventalin.Codalmacen equals almacen.Codalmacen
                join rem in _context.RemCajasfronts on new
                {
                    Ventas = EF.Functions.Collate(almacen.Codalmacen, "Modern_Spanish_CI_AS")
                } equals new
                {
                    Ventas = rem.Codalmventas
                }
                join remFront in _context.RemFronts on rem.Idfront equals remFront.Idfront
                join vendedor in _context.Vendedores on ventalin.Codvendedor equals vendedor.Codvendedor
                where ventacab.Fecha >= initDate && ventacab.Fecha <= endDate && remFront.Titulo.ToUpper().Contains("RW") && !ventacab.Numserie.ToUpper().Contains("%I") && ventalin.Tipo.Contains("V") && ventalin.Lineaoculta.Contains("F")
                group new { ventalin, art, ventacab, almacen, rem, remFront, vendedor } by new { remFront.Titulo, vendedor.Nomvendedor, art.Descripcion, ventalin.Preciodefecto } into x
                orderby x.Key.Descripcion ascending
                select new Filtro()
                {
                    Name = x.Key.Descripcion,
                    Checked = false,


                };
            return venta.Distinct().ToList();
        }


        public List<Reporte> GetReporteV(DateTime Date)
        {
            List<Reporte> reportes = new List<Reporte>();
            SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SPS_INV_VESP_REPORTE";
            cmd.Parameters.Add("@FECHA", System.Data.SqlDbType.VarChar,10).Value = Date.ToString("dd/MM/yyyy");
            cmd.CommandTimeout = 120;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Reporte repp = new Reporte();
                repp.cod = (string)reader["COD"];
                repp.Sucursal = (string)reader["SUCURSAL"];
                repp.Articulo = (string)reader["ARTICULO"];
                repp.Seccion = (string)reader["SECCION"];
                repp.InvAyer = (string)reader["INVAYER"];
                repp.ConsumoAyer = (double)reader["CONSUMOAYER"];
                repp.TraspasoAyer = (double)reader["TRASPASOAYER"];
                repp.InvHoy = (string)reader["INVHOY"];
                repp.Captura = (DateTime)reader["CAPTURA"];
                repp.InvFormula = (double)reader["INVFORMULA"];
                repp.Diferencia = (double)reader["DIFERENCIA"];
                reportes.Add(repp);
            }
            connection.Close();
            //var basesuc =
            //    from almacen in _context.Almacens
            //    orderby almacen.Codalmacen ascending
            //    where (almacen.Notas.Contains("RW"))
            //    select new Reporte()
            //    {
            //        cod = almacen.Codalmacen,
            //        Sucursal = almacen.Nombrealmacen,

            //    };

            //var artreg =
            //    from art in _context.Articulos1
            //    join camp in _context.Articuloscamposlibres on art.Codarticulo equals camp.Codarticulo
            //    join sec in _context.Secciones on art.Seccion equals sec.Numseccion /////////---seccion
            //    orderby art.Codarticulo ascending
            //    where (camp.RegularizaSemanal == "T")
            //    select new Reporte() { Articulo = art.Descripcion, Seccion = sec.Descripcion };

            //var CrossJoinResult =
            //    from sucursales in basesuc
            //    from articulos in artreg
            //    select new Reporte()
            //    {
            //        cod = sucursales.cod,
            //        Sucursal = sucursales.Sucursal,
            //        Articulo = articulos.Articulo,
            //        Seccion = articulos.Seccion, /////////---seccion
            //        InvAyer = "0",
            //        InvHoy = "0",
            //        ConsumoAyer = 0,
            //        TraspasoAyer = 0,
            //        InvFormula = 0,
            //        Captura = DateTime.Now.Date,

            //    };

            //var inv =
            //    from moviment in _context.Moviments
            //    join articulo1 in _context.Articulos1 on moviment.Codarticulo equals articulo1.Codarticulo
            //    join campos in _context.Articuloscamposlibres on articulo1.Codarticulo equals campos.Codarticulo
            //    join almac in _context.Almacens on moviment.Codalmacenorigen equals almac.Codalmacen
            //    where (moviment.Fecha.Value.Date == Date.Date) && (moviment.Hora.Value.Hour >= 1 && moviment.Hora.Value.Hour <= 3)
            //    where (campos.RegularizaSemanal == "T") && (moviment.Tipo == "REG")
            //    orderby articulo1.Descripcion ascending, moviment.Codalmacenorigen ascending
            //    group new { moviment, almac, articulo1 } by new { moviment.Codalmacenorigen, almac.Nombrealmacen, articulo1.Descripcion, moviment.Unidades, moviment.Hora } into x
            //    select new Reporte()
            //    {
            //        cod = x.Key.Codalmacenorigen,
            //        Sucursal = x.Key.Nombrealmacen,
            //        Articulo = x.Key.Descripcion,
            //        InvHoy = x.First().moviment.Unidades.Value.ToString(),
            //        Captura = x.Key.Hora.Value,

            //        //Description = articulo1.Descripcion,
            //        //Price = moviment.Precio.Value,
            //        //Unity = moviment.Unidades.Value,
            //        //UnitMeasure = articulo1.Unidadmedida
            //    };

            //var invAyer =
            //    from moviment in _context.Moviments
            //    join articulo1 in _context.Articulos1 on moviment.Codarticulo equals articulo1.Codarticulo
            //    join campos in _context.Articuloscamposlibres on articulo1.Codarticulo equals campos.Codarticulo
            //    join almac in _context.Almacens on moviment.Codalmacenorigen equals almac.Codalmacen
            //    where (moviment.Fecha.Value.Date == Date.AddDays(-1).Date) && (moviment.Hora.Value.Hour >= 1 && moviment.Hora.Value.Hour <= 3)
            //    where (campos.RegularizaSemanal == "T") && (moviment.Tipo == "REG")
            //    orderby articulo1.Descripcion ascending, moviment.Codalmacenorigen ascending
            //    group new { moviment, almac, articulo1 } by new { moviment.Codalmacenorigen, almac.Nombrealmacen, articulo1.Descripcion, moviment.Unidades, moviment.Hora } into x
            //    select new Reporte()
            //    {
            //        cod = x.Key.Codalmacenorigen,
            //        Sucursal = x.Key.Nombrealmacen,
            //        Articulo = x.Key.Descripcion,
            //        InvAyer = x.First().moviment.Unidades.Value.ToString()

            //        //Description = articulo1.Descripcion,
            //        //Price = moviment.Precio.Value,
            //        //Unity = moviment.Unidades.Value,
            //        //UnitMeasure = articulo1.Unidadmedida
            //    };






            //var consumo =
            //    from albvt in _context.Albventaconsumos
            //    join albvtcab in _context.Albventacabs on new
            //    {
            //        albvt.Numserie,
            //        albvt.Numalbaran,
            //        albvt.N
            //    }
            //    equals new
            //    {
            //        albvtcab.Numserie,
            //        albvtcab.Numalbaran,
            //        albvtcab.N
            //    }
            //    join art in _context.Articulos1 on albvt.Codarticulo equals art.Codarticulo
            //    join almac in _context.Almacens on albvt.Codalmacen equals almac.Codalmacen
            //    join artcamp in _context.Articuloscamposlibres on albvt.Codarticulo equals artcamp.Codarticulo
            //    where (albvtcab.Fecha.Value.Date == Date.AddDays(-1).Date) && (artcamp.RegularizaSemanal == "T")
            //    group new { albvt, albvtcab, almac, art, artcamp } by new { almac.Nombrealmacen, art.Descripcion } into x
            //    select new Reporte()
            //    {

            //        Sucursal = x.Key.Nombrealmacen,
            //        Articulo = x.Key.Descripcion,
            //        ConsumoAyer = x.Sum(cn => cn.albvt.Consumo.Value),

            //        //Description = articulo1.Descripcion,
            //        //Price = moviment.Precio.Value,
            //        //Unity = moviment.Unidades.Value,
            //        //UnitMeasure = articulo1.Unidadmedida
            //    };
            //var compras =
            //    from albcomp in _context.Albcompralins
            //    join almac in _context.Almacens on albcomp.Codalmacen equals almac.Codalmacen
            //    join art in _context.Articulos1 on albcomp.Codarticulo equals art.Codarticulo
            //    join albcompcab in _context.Albcompracabs on new
            //    {
            //        albcomp.Numserie,
            //        albcomp.Numalbaran,
            //        albcomp.N
            //    }
            //    equals new
            //    {
            //        albcompcab.Numserie,
            //        albcompcab.Numalbaran,
            //        albcompcab.N
            //    }
            //    join artcamp in _context.Articuloscamposlibres on art.Codarticulo equals artcamp.Codarticulo
            //    where (albcompcab.Fechaalbaran.Value.Date == Date.AddDays(-1).Date) && (artcamp.RegularizaSemanal == "T")
            //    group new { albcomp, albcompcab, almac, art, artcamp } by new { almac.Nombrealmacen, art.Descripcion } into x
            //    select new Reporte()
            //    {
            //        Sucursal = x.Key.Nombrealmacen,
            //        Articulo = x.Key.Descripcion,
            //        TraspasoAyer = x.Sum(cn => cn.albcomp.Unidadestotal.Value),

            //    };

            //var list0 = from ayer in CrossJoinResult
            //            join hoy in invAyer.Distinct() on new { ayer.Sucursal, ayer.Articulo } equals new { hoy.Sucursal, hoy.Articulo } into inventarios
            //            from ed in inventarios.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = ayer.cod,
            //                Sucursal = ayer.Sucursal,
            //                Articulo = ayer.Articulo,
            //                Seccion = ayer.Seccion, /////////---seccion
            //                InvAyer = ed.InvAyer == null ? "SIN CAPTURA" : ed.InvAyer,


            //            };

            //var list1 = from ayer in list0
            //            join hoy in inv on new { ayer.Sucursal, ayer.Articulo } equals new { hoy.Sucursal, hoy.Articulo } into inventarios
            //            from ed in inventarios.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = ayer.cod,
            //                Sucursal = ayer.Sucursal,
            //                Articulo = ayer.Articulo,
            //                Seccion = ayer.Seccion, /////////---seccion
            //                InvAyer = ayer.InvAyer,
            //                InvHoy = ed.InvHoy == null ? "SIN CAPTURA" : ed.InvHoy,
            //                Captura = ed.Captura == null ? DateTime.Now.Date : ed.Captura,

            //            };
            //var list2 = from x in list1
            //            join z in consumo on new { x.Sucursal, x.Articulo } equals new { z.Sucursal, z.Articulo } into xz
            //            from ed in xz.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = x.cod,
            //                Sucursal = x.Sucursal,
            //                Articulo = x.Articulo,
            //                Seccion = x.Seccion, /////////---seccion
            //                InvAyer = x.InvAyer,
            //                InvHoy = x.InvHoy,
            //                ConsumoAyer = ed.ConsumoAyer == null ? 0 : ed.ConsumoAyer,
            //                Captura = x.Captura,

            //            };
            //var list3 = from x in list2
            //            join z in compras on new { x.Sucursal, x.Articulo } equals new { z.Sucursal, z.Articulo } into xz
            //            from ed in xz.DefaultIfEmpty()

            //            select new Reporte()
            //            {
            //                cod = x.cod,
            //                Sucursal = x.Sucursal,
            //                Articulo = x.Articulo,
            //                Seccion = x.Seccion, /////////---seccion
            //                InvAyer = x.InvAyer,
            //                InvHoy = x.InvHoy,
            //                ConsumoAyer = x.ConsumoAyer,
            //                TraspasoAyer = ed.TraspasoAyer == null ? 0 : ed.TraspasoAyer,
            //                InvFormula = (double.Parse((x.InvAyer == "SIN CAPTURA" ? "0" : x.InvAyer)) + (ed.TraspasoAyer == null ? 0 : ed.TraspasoAyer)) - (x.ConsumoAyer),
            //                Diferencia = ((double.Parse(x.InvHoy == "SIN CAPTURA" ? "0" : x.InvHoy)) - ((double.Parse((x.InvAyer == "SIN CAPTURA" ? "0" : x.InvAyer)) + (ed.TraspasoAyer == null ? 0 : ed.TraspasoAyer)) - (x.ConsumoAyer))),
            //                Captura = x.Captura,
            //            };


            ////return inv.Distinct().ToList();
            ////var list4 = list3.GroupBy(x => new { x.Sucursal, x.Articulo }).Select(x => x.First()).ToList();


            //return list3.OrderBy(x => x.Articulo).ThenBy(x => x.cod).ToList();

            
            return reportes;
        }

        public List<Apps> GetReporteApps(DateTime DateI, DateTime DateF)
        {
            List<Apps> reportes = new List<Apps>();
            SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SPS_VENTA_APPS";
            cmd.Parameters.Add("@FECHAINI", System.Data.SqlDbType.VarChar, 10).Value = DateI.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@FECHAFIN", System.Data.SqlDbType.VarChar, 10).Value = DateF.ToString("dd-MM-yyyy");
            cmd.CommandTimeout = 120;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Apps repp = new Apps();
                repp.Reg = (string)reader["COD"];
                repp.Cod = (string)reader["REG"];
                repp.Sucursal = (string)reader["SUCURSAL"];
                repp.Codcliente = (int)reader["CODCLIENTE"];
                repp.App = (string)reader["APP"];
                repp.Total = (double)reader["TOTAL"];
                repp.Mes = (string)reader["MES"];
                repp.Marca = (string)reader["MARCA"];
                repp.Nom = (string)reader["NOM"];
                reportes.Add(repp);
            }
            connection.Close();



            return reportes;
        }

        public List<SucursalesFront> GetSucursalesF()
        {
            List<SucursalesFront> reportes = new List<SucursalesFront>();
            SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SPS_SUCURSALES";
            cmd.CommandTimeout = 120;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SucursalesFront repp = new SucursalesFront();
                repp.Idfront = (string)reader["COD"];
                repp.Titulo = (string)reader["SUCURSAL"];
                reportes.Add(repp);
            }
            connection.Close();



            return reportes;
        }

        public List<Ranking> GetRkg(string branch, DateTime initDate, DateTime endDate)
        {
            List<Ranking> reportes = new List<Ranking>();
            SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SPS_RANKING";
            cmd.Parameters.Add("@FECHAINI", System.Data.SqlDbType.VarChar, 10).Value = initDate.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@FECHAFIN", System.Data.SqlDbType.VarChar, 10).Value = endDate.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@SERIE", System.Data.SqlDbType.VarChar, 2).Value = branch.ToString();
            cmd.CommandTimeout = 120;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Ranking repp = new Ranking();
                repp.Cod = (int)reader["COD"];
                repp.Descripcion = (string)reader["DESCRIPCION"];
                repp.Seccion = (string)reader["SECCION"];
                repp.Unidades = (double)reader["UDS"];
                repp.Importe = (double)reader["IMPORTE"];
                repp.Porcentaje = (double)reader["PORCENTAJE"];
                reportes.Add(repp);
            }
            connection.Close();



            return reportes;
        }

        public List<Checadas> GetReporteChecadas(DateTime DateI, DateTime DateF)
        {
            List<Checadas> reportes = new List<Checadas>();
            SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SPS_CHECADAS_REG";
            cmd.Parameters.Add("@FECHAINI", System.Data.SqlDbType.VarChar, 10).Value = DateI.ToString("dd/MM/yyyy");
            cmd.Parameters.Add("@FECHAFIN", System.Data.SqlDbType.VarChar, 10).Value = DateF.ToString("dd/MM/yyyy");
            cmd.CommandTimeout = 120;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Checadas repp = new Checadas();
                repp.Fecha = (DateTime)reader["FechaHora"];
                repp.Reloj = (string)reader["Reloj"];
                repp.Empleado = (string)reader["Empleado"];
                

                reportes.Add(repp);
            }
            connection.Close();



            return reportes;
        }

    }
}
