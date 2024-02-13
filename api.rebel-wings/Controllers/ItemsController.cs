using api.rebel_wings.ActionFilter;
using api.rebel_wings.Models.ApiResponse;
using api.rebel_wings.Models.Articulos;
using AutoMapper;
using biz.rebel_wings.Services.Logger;
using dal.bd2.DBContext;
using dal.rebel_wings.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Web.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api.rebel_wings.Controllers
{
    /// <summary>
    /// Controlador para Articulos de tienda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ItemsController : ControllerBase
    {
        private readonly biz.bd1.Repository.Articulos.IArticulosRespository _articulosRespositoryBD1;
        private readonly biz.bd2.Repository.Articulos.IArticulosRepository _articulosRespositoryBD2;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        protected BD2Context _contextdb2;
        public Db_Rebel_WingsContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="articulosRespositoryBD1"></param>
        /// <param name="articulosRespositoryBD2"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ItemsController(biz.bd1.Repository.Articulos.IArticulosRespository articulosRespositoryBD1,
            biz.bd2.Repository.Articulos.IArticulosRepository articulosRespositoryBD2,
            IMapper mapper,
            ILoggerManager logger,
            BD2Context db2c,
            Db_Rebel_WingsContext context
            )
        {
            _articulosRespositoryBD1 = articulosRespositoryBD1;
            _articulosRespositoryBD2 = articulosRespositoryBD2;
            _mapper = mapper;
            _logger = logger;
            _contextdb2 = db2c;
            _context = context;
        }
        /// <summary>
        ///  LISTA de Articulos
        /// </summary>
        /// <param name="id">ID de Sucursal</param>
        /// <param name="word">Palabra CLAVE para la busqueda</param>
        /// <returns></returns>
        [HttpGet("{id}/{word}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<ApiResponse<List<ItemsDto>>>> Get(int id, string word)
        {
            ApiResponse<List<List<ItemsDto>>> response = new ApiResponse<List<List<ItemsDto>>>();
            try
            {
                word = word.ToUpper();
                List<List<ItemsDto>> articulos = new List<List<ItemsDto>>();
                var repository = _mapper.Map<List<ItemsDto>>(_articulosRespositoryBD1.GetAll()
                    .Where(x => !x.Descripcion.StartsWith("*") && x.Descatalogado.Equals("F")).Select(s => new ItemsDto()
                    {
                        Codarticulo = s.Codarticulo,
                        Descripcion = s.Descripcion
                    }).ToList());
                repository.Union(_mapper.Map<List<ItemsDto>>(_articulosRespositoryBD2.GetAll()
                    .Where(x => !x.Descripcion.StartsWith("*") && x.Descatalogado.Equals("F")).Select(s => new ItemsDto()
                    {
                        Codarticulo = s.Codarticulo,
                        Descripcion = s.Descripcion
                    }).ToList())).ToList();
                
                var customerQueryList = word.ToCharArray();
                string init = "";
                for (int i = 0; i < customerQueryList.Length; i++)
                {
                    init = $"{init}{customerQueryList[i].ToString()}";
                    Console.WriteLine($"WORD : {init}");
                    // var res = _articulosRespositoryBD1.GetAll()
                    //     .Where(x => x.Descripcion.StartsWith("*") && x.Descripcion.Contains(init) && x.Descatalogado.Equals("F"))
                    //     .Select(s => new ItemsDto()
                    //     {
                    //         Codarticulo = s.Codarticulo,
                    //         Descripcion = s.Descripcion
                    //     }).ToList();
                    var res = repository
                        .Where(x => x.Descripcion.Contains(init))// !x.Descripcion.StartsWith("*")  cuando la conexión sea establezca cambiear por esta linea
                        .Select(s => new ItemsDto()
                        {
                            Codarticulo = s.Codarticulo,
                            Descripcion = s.Descripcion
                        }).ToList();
                    articulos.Add(res);
                }
                // articulos.Add(repository);
                response.Result = articulos;
                response.Success = true;
                response.Message = "Consult was success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }
            return StatusCode(200, response);
        }


        [HttpGet]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetItems()
        {
            try
            {
                var repository = _articulosRespositoryBD2.GetAll() 
                    .Where(x => !x.Descripcion.Trim().StartsWith("*") && x.Descatalogado.Equals("F")).Select(s => new
                    {
                        Codarticulo = s.Codarticulo,
                        Descripcion = s.Descripcion,
                        Marca = s.Marca

                    }).ToList();

               
                return StatusCode(200, new { Result = repository,
                Success = true,
                Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                return StatusCode(500, new { Success = false,
                Message = ex.ToString(),
            });
            }
        }

        [HttpGet("{id}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetItem(int id)
        {
            try
            {
                var repository = _articulosRespositoryBD2.GetAll()
                    .Where(x => !x.Descripcion.Trim().StartsWith("*") && x.Descatalogado.Equals("F") && x.Codarticulo == id).Select(s => new
                    {
                        Codarticulo = s.Codarticulo.ToString(),
                        Descripcion = s.Descripcion.ToString(),
                        Referencia = s.Refproveedor.ToString(),
                        Departamento = s.Dpto.ToString(),
                        Seccion = s.Seccion.ToString(),
                        Marca = s.Marca.ToString(),
                        UsaStock = s.Usastocks.ToString(),
                        ForzarUdsEntreras = s.Forzarudsenterasventa.ToString(),
                        TipoImpuesto = s.Tipoimpuesto.ToString(),
                        ImpuestoCompra = s.Impuestocompra.ToString(),
                        Familia = s.Familia.ToString(),
                        subfamilia = s.Subfamilia.ToString(),
                        linea = s.Linea.ToString(),

                    });


                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }


        [HttpGet]
        [Route("getDepartamentos")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetDepartamentos()
        {
            try
            {
                var repository = _contextdb2.Departamentos.Select(s => new
                {
                    numdpto = s.Numdpto,
                    descripcion = s.Descripcion
                }).ToList(); 

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }


        [HttpGet]
        [Route("getSecciones")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetSeccion()
        {
            try
            {
                var repository = _contextdb2.Secciones.Select(s => new 
                {
                    numseccion = s.Numseccion,
                    descripcion = s.Descripcion,
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet]
        [Route("getMarcas")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetMarcas()
        {
            try
            {
                var repository = _contextdb2.Marcas.Select(s => new
                {
                    codmarca = s.Codmarca,
                    descripcion = s.Descripcion
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }


        [HttpGet]
        [Route("getImpuestos")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetImpuestos()
        {
            try
            {
                var repository = _contextdb2.Impuestos.Select(s => new
                {
                    tipoiva = s.Tipoiva,
                    descripcion = s.Descripcion
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }


        [HttpGet]
        [Route("getFamilias")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetFamilias()
        {
            try
            {
                var repository = _contextdb2.Familias.Select(s => new
                {
                    num = s.Numfamilia,
                    descripcion = s.Descripcion
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet]
        [Route("getSubfamilias")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetSubfamilias()
        {
            try
            {
                var repository = _contextdb2.Subfamilias.Select(s => new
                {
                    num = s.Numsubfamilia,
                    descripcion = s.Descripcion
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet]
        [Route("getlineas")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetLineas()
        {
            try
            {
                var repository = _contextdb2.Lineas.Select(s => new
                {
                    tipoiva = s.Codlinea,
                    descripcion = s.Descripcion
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet]
        [Route("getArticulolin/{id}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> Getarticulolin(int id)
        {
            try
            {
                var repository = _contextdb2.Articuloslins.Where(x => x.Codarticulo == id).Select(s => new
                {
                    codarticulo = s.Codarticulo,
                    costemedio = s.Costemedio,
                    costestock = s.Costestock,
                    ultimocoste = s.Ultimocoste,
                    precioultcompra = s.Precioultcompra
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet]
        [Route("getPreciosventa/{id}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> Getpreciosventa(int id)
        {
            try
            {
                var repository = from a in _contextdb2.Preciosventa.Where(x => x.Codarticulo == id && x.Idtarifav == 5) join b in _contextdb2.Tarifasventa on a.Idtarifav equals b.Idtarifav select new
                {
                    codarticulo = a.Codarticulo,
                    idtarifav = a.Idtarifav,
                    pneto = a.Pneto,
                    descripcion = b.Descripcion
                }; 

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }


        [HttpGet]
        [Route("getPrecioscompra/{id}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetpreciosCompra(int id)
        {
            try
            {
                var repository = _contextdb2.Precioscompras.Where(x => x.Codarticulo == id).Select(s => new
                {
                    codarticulo = s.Codarticulo,
                    codproveedor = s.Codproveedor,
                    idtarifac = s.Idtarifac,
                    pbruto = s.Pbruto,
                    dto = s.Dto,
                    pneto = s.Pneto,
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet]
        [Route("getTarifascompra")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetTarifasCompra()
        {
            try
            {
                var repository = _contextdb2.Tarifascompras.Select(s => new
                {
                    descripcion = s.Descripcion,
                    codigo = s.Idtarifac,
                    desde = s.Fechaini,
                    hasta = s.Fechafin
                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }



        [HttpGet]
        [Route("getProveedores")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetProveedores()
        {
            try
            {
                var repository = _contextdb2.Proveedores.Select(s => new
                {
                    codproveedor= s.Codproveedor,
                    nombre = s.Nomproveedor,

                }).ToList();

                return StatusCode(200, new
                {
                    Result = repository,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }


        [HttpPost]
        [Route("InsertarCambios")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> insertCambios([FromBody] InsertModel model)
        {
            try
            {
               
                using (SqlConnection connection = (SqlConnection)_contextdb2.Database.GetDbConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_INSERT_CONTROL_MODIFICACIONES_INVENTARIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@IDU", model.idu);
                        command.Parameters.AddWithValue("@JSONDATA", model.jsondata);
                        command.Parameters.AddWithValue("@JUSTIFICACION", model.justificacion);
                        command.Parameters.AddWithValue("@ACCION", model.accion);

                        // Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();

                        return StatusCode(200, new
                        {
                            Result = "",
                            Success = true,
                            Message = "Consult was success",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }


        [HttpGet]
        [Route("getCambiosArticulos")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetCambios()
        {
            try
            {
                List<CambiosModel> listacambios = new List<CambiosModel>();

                using (SqlConnection connection = (SqlConnection)_contextdb2.Database.GetDbConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GET_CONTROL_MODIFICACIONES_INVENTARIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CambiosModel model = new CambiosModel();
                                // Accede a los datos de cada fila
                                int id = reader.GetInt32(reader.GetOrdinal("ID"));
                                int idu = reader.GetInt32(reader.GetOrdinal("IDU"));
                                string jsonData = reader.GetString(reader.GetOrdinal("JSONDATA"));
                                string justificacion = reader.GetString(reader.GetOrdinal("JUSTIFICACION"));
                                DateTime fecha = reader.GetDateTime(reader.GetOrdinal("FECHA"));
                                bool activo = reader.GetBoolean(reader.GetOrdinal("ACTIVO"));
                                string accion = reader.GetString(reader.GetOrdinal("ACCION"));

                               model.id = id;
                               model.idu = idu;
                                model.accion = accion;
                                model.fecha = fecha;
                                model.activo = activo;  
                                model.justificacion = justificacion;    
                                model.jsondata = jsonData;

                                var usuario = _context.Users.Find(idu);
                                model.username = usuario == null ? "" : usuario.Name +" " +usuario.LastName;
                                listacambios.Add(model);
                                
                            }
                        }
                    }
                }
                return StatusCode(200, new
                {
                    Result = listacambios,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet]
        [Route("getCambiosArticulosH")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetCambiosH()
        {
            try
            {
                List<CambiosModel> listacambios = new List<CambiosModel>();

                using (SqlConnection connection = (SqlConnection)_contextdb2.Database.GetDbConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GET_CONTROL_MODIFICACIONES_INVENTARIO_H", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CambiosModel model = new CambiosModel();
                                // Accede a los datos de cada fila
                                int id = reader.GetInt32(reader.GetOrdinal("ID"));
                                int idu = reader.GetInt32(reader.GetOrdinal("IDU"));
                                string jsonData = reader.GetString(reader.GetOrdinal("JSONDATA"));
                                string justificacion = reader.GetString(reader.GetOrdinal("JUSTIFICACION"));
                                DateTime fecha = reader.GetDateTime(reader.GetOrdinal("FECHA"));
                                bool activo = reader.GetBoolean(reader.GetOrdinal("ACTIVO"));
                                string accion = reader.GetString(reader.GetOrdinal("ACCION"));

                                model.id = id;
                                model.idu = idu;
                                model.accion = accion;
                                model.fecha = fecha;
                                model.activo = activo;
                                model.justificacion = justificacion;
                                model.jsondata = jsonData;

                                var usuario = _context.Users.Find(idu);
                                model.username = usuario == null ? "" : usuario.Name + " " + usuario.LastName;
                                listacambios.Add(model);

                            }
                        }
                    }
                }
                return StatusCode(200, new
                {
                    Result = listacambios,
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }



        [HttpPost]
        [Route("descartarCambios/{id}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> deleteCambios(int id)
        {
            try
            {

                using (SqlConnection connection = (SqlConnection)_contextdb2.Database.GetDbConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DELETE_CONTROL_MODIFICACIONES_INVENTARIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@ID", id);
                        // Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();

                        return StatusCode(200, new
                        {
                            Result = "",
                            Success = true,
                            Message = "Consult was success",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }



        [HttpPost]
        [Route("UpdateData")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateData([FromBody] UpdateDataModel model)
        {
            try
            {
                SqlConnection conn = (SqlConnection)_contextdb2.Database.GetDbConnection();
                conn.Open();
                dynamic data = JsonConvert.DeserializeObject(model.jsondata);
                var olddata = data.data; 
                var newdata = data.newdata;
                if (model.tipo == 1)
                {
                    var temp = olddata.codarticulo;
                    int codarticulo = (int)olddata.codarticulo;
                    string descripcion = newdata.descripcion;
                    int departamento = (int)newdata.departamento;
                    int seccion = (int)newdata.seccion;
                    int marca = (int)newdata.marca;
                    int familia = newdata.familia == null ? 0 : (int)newdata.familia;
                    int subfamilia = newdata.subfamilia == null ? 0 : (int)newdata.subfamilia;
                    bool usastock = (bool)newdata.usastock;
                    string usastockstring = usastock ? "T" : "F";
                    bool udsenteras = (bool)newdata.udsenteras;
                    int tipoimpuesto = (int)newdata.tipoimpuesto;
                    int impuestoCompra = (int)newdata.impuestocompra;

                        using (SqlCommand command = new SqlCommand("SP_UPDATE_ITEM", conn))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            // Agregar los parámetros al comando
                            command.Parameters.AddWithValue("@CODART", codarticulo);
                            command.Parameters.AddWithValue("@DESCRIPCION", descripcion);
                            command.Parameters.AddWithValue("@DEPARTAMENTO", departamento);
                            command.Parameters.AddWithValue("@SECCION", seccion);
                            command.Parameters.AddWithValue("@MARCA", marca);
                            command.Parameters.AddWithValue("@USASTOCK", usastockstring);
                            command.Parameters.AddWithValue("@UDSENTERAS", udsenteras);
                            command.Parameters.AddWithValue("@TIPOIMPUESTO", tipoimpuesto);
                            command.Parameters.AddWithValue("@IMPUESTOCOMPRA", impuestoCompra);
                            command.Parameters.AddWithValue("@FAMILIA", familia);
                            command.Parameters.AddWithValue("@SUBFAMILIA", subfamilia);

                            // Ejecutar el comando
                            int rowsAffected = command.ExecuteNonQuery();
                        }
                }
                if (model.tipo == 2)
                {
                    int codarticulo = olddata.articulolin[0].codarticulo;
                    double costemedio = (double)newdata.costemedio;
                    double costestock = (double)newdata.costestock;
                    double ultimocoste = (double)newdata.ultimocoste;
                    double preciocompra = (double)newdata.preciocompra;

                        using (SqlCommand command = new SqlCommand("SP_UPDATE_ARTICULOLIN", conn))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            // Agregar los parámetros al comando
                            command.Parameters.AddWithValue("@CODART", codarticulo);
                            command.Parameters.AddWithValue("@COSTEMEDIO", costemedio);
                            command.Parameters.AddWithValue("@COSTESTOCK", costestock);
                            command.Parameters.AddWithValue("@ULTIMOCOSTE", ultimocoste);
                            command.Parameters.AddWithValue("@PRECIOCOMPRA", preciocompra);

                            // Ejecutar el comando
                            int rowsAffected = command.ExecuteNonQuery();
                        }


                    foreach (var item in newdata.preciosv) 
                    {
                        double pneto = (double)item.pneto;

                            using (SqlCommand command = new SqlCommand("SP_UPDATE_PRECIOSVENTA", conn))
                            {
                                command.CommandType = System.Data.CommandType.StoredProcedure;

                                // Agregar los parámetros al comando
                                command.Parameters.AddWithValue("@CODART", codarticulo);
                                command.Parameters.AddWithValue("@PNETO", pneto);

                                // Ejecutar el comando
                                int rowsAffected = command.ExecuteNonQuery();
                            }

                    }
                    
                }
                if (model.tipo == 3)
                {
                    int codproveedor = (int)olddata[0].codproveedor;
                    int idtarifac = (int)olddata[0].idtarifac;
                    int codarticulo = (int)olddata[0].codarticulo;
                    double pneto = (double)newdata.pbruto;

                        using (SqlCommand command = new SqlCommand("SP_UPDATE_PRECIOSCOMPRA", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Configurar parámetros del procedimiento almacenado
                            command.Parameters.Add("@CODART", SqlDbType.Int).Value = codarticulo;
                            command.Parameters.Add("@PNETO", SqlDbType.Float).Value = pneto;
                            command.Parameters.Add("@IDTARIFAC", SqlDbType.Int).Value = idtarifac;
                            command.Parameters.Add("@CODPROVEEDOR", SqlDbType.Int).Value = codproveedor;

                            // Ejecutar el procedimiento almacenado
                            int rowsAffected = command.ExecuteNonQuery();
                        }
                }
                conn.Close();
                return StatusCode(200, new
                {
                    Result = "",
                    Success = true,
                    Message = "Consult was success",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }



        [HttpPost]
        [Route("ConfirmarCambios/{id}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> ConfirmarCambios(int id)
        {
            try
            {

                using (SqlConnection connection = (SqlConnection)_contextdb2.Database.GetDbConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UPDATE_CONTROL_ARTICULOS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@ID", id);
                        // Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();

                        return StatusCode(200, new
                        {
                            Result = "",
                            Success = true,
                            Message = "Consult was success",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.ToString(),
                });
            }
        }



    }

    public class InsertModel 
    {
        public int idu { get; set; }
        public string jsondata { get; set; }
        public string justificacion { get; set; }
        public string accion { get; set; }

    }

    public class CambiosModel
    {
        public int id { get; set; }
        public int idu { get; set; }
        public string jsondata { get; set; }
        public string justificacion { get; set; }
        public string accion { get; set; }
        public DateTime fecha { get; set; }
        public bool activo { get; set; }
        public string username { get; set; }

    }

    public class UpdateDataModel 
    {
        public string jsondata { get; set; }
        public int tipo { get; set; }
    }

}
