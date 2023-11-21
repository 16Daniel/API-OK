using api.rebel_wings.ActionFilter;
using api.rebel_wings.Models.ApiResponse;
using api.rebel_wings.Models.Dashboard;
using api.rebel_wings.Models.Mermas;
using api.rebel_wings.Models.Implementacion;
using api.rebel_wings.Models.RequestTransfer;
using AutoMapper;
using biz.bd2.Repository.Stock;
using biz.rebel_wings.Repository.Implementacion;
using biz.fortia.Models;
using biz.fortia.Repository.RH;
using biz.rebel_wings.Models.Transfer;
using biz.rebel_wings.Repository.Dashboard;
using biz.rebel_wings.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using biz.rebel_wings.Entities;

namespace api.rebel_wings.Controllers;
/// <summary>
/// Controller to Dashboard
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DashboardController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IRHTrabRepository _iRHTrabRepository;
    private readonly IStockRepository _stockDB2Repository;
    private readonly biz.bd1.Repository.Stock.IStockRepository _stockDB1Repository;
    private readonly biz.bd1.Repository.Sucursal.ISucursalRepository _sucursalDB1Repository;
    private readonly biz.bd2.Repository.Sucursal.ISucursalRepository _sucursalDB2Repository;
    private readonly biz.rebel_wings.Repository.Implementacion.ITiemposRepository _tiemposRepository;
    private readonly biz.rebel_wings.Repository.Implementacion.I25ptsRepository _i25ptsRepository;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dashboardRepository"></param>
    /// <param name="iRhTrabRepository"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public DashboardController(ILoggerManager logger,
        IMapper mapper, 
        IDashboardRepository dashboardRepository,
        IRHTrabRepository iRhTrabRepository,
        IStockRepository stockDB2Repository,
        biz.bd1.Repository.Stock.IStockRepository stockDB1Repository,
        biz.bd1.Repository.Sucursal.ISucursalRepository sucursalDB1Repository,
        biz.bd2.Repository.Sucursal.ISucursalRepository sucursalDB2Repository,
        ITiemposRepository tiemposRepository,
        I25ptsRepository i25ptsRepository)
    {
        _dashboardRepository = dashboardRepository;
        _iRHTrabRepository = iRhTrabRepository;
        _logger = logger;
        _mapper = mapper;
        _stockDB2Repository = stockDB2Repository;
        _stockDB1Repository = stockDB1Repository;
        _sucursalDB1Repository = sucursalDB1Repository;
        _sucursalDB2Repository = sucursalDB2Repository;
        _tiemposRepository = tiemposRepository;
        _i25ptsRepository = i25ptsRepository;
    }
    /// <summary>
    /// GET:
    /// Return info to admin page
    /// </summary>
    /// <returns></returns>
    [HttpGet("Admin")]
    [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<DashboardAdmin>> GetAdmin([FromQuery] DateTime dateTime)
    {
        var response = new ApiResponse<DashboardAdmin>();
        try
        {
            var list = _mapper.Map<List<TransfersListDto>>(_iRHTrabRepository.GetBranchList());

            response.Result = _mapper.Map<DashboardAdmin>(_dashboardRepository.GetAdmin(dateTime.AbsoluteStart(),
                dateTime.AbsoluteEnd(), _mapper.Map<List<TransfersList>>(list)));
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
    /// <summary>
    /// GET:
    /// Supervisor
    /// </summary>
    /// <param name="id">Branch ID</param>
    /// <param name="dateTime">Time frame 1</param>
    /// <param name="dateTime">Time frame 2</param>
    /// <param name="int">If task is done or not</param>
    /// <param name="city">City ID</param>
    /// <returns></returns>
    [HttpGet("{id}/Supervisor")]
    [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<DashboardSupervisor>> GetSupervisor(int id, [FromQuery] DateTime timeOne,
        [FromQuery] DateTime timeTwo, [FromQuery] int isDone, [FromQuery] int city)
    {
        var response = new ApiResponse<DashboardSupervisor>();
        try
        {
            response.Result =
                _mapper.Map<DashboardSupervisor>(
                    _dashboardRepository.GetSupervisorsV2(
                        id, timeOne.AbsoluteStart(), timeTwo.AbsoluteEnd(), isDone, city)
                    );
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
    /// <summary>
    /// GET:
    /// Regional
    /// </summary>
    /// <param name="id">Branch</param>
    /// <param name="timeOne">Time frame 1</param>
    /// <param name="timeTwo">Time frame 2</param>
    /// <param name="isDone">is done</param>
    /// <param name="city">City</param>
    /// <returns></returns>
    [HttpGet("{id}/Regional")]
    [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<DashboardRegional>> GetRegionals(int id, [FromQuery] DateTime timeOne, 
        [FromQuery] DateTime timeTwo, [FromQuery] int isDone, [FromQuery] int city)
    {
        var response = new ApiResponse<DashboardRegional>();
        try
        {
            response.Result = _mapper.Map<DashboardRegional>(_dashboardRepository.GetRegionalV2
                (id, timeOne.AbsoluteStart(), timeTwo.AbsoluteEnd(), isDone, city));
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

    /// <summary>
    /// GET:
    /// Asistencias
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    [HttpGet("{id}/Assistance")]
    [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<DashboardAssistanceV2>> GetAssistance(int id,  [FromQuery] DateTime timeOne, 
        [FromQuery] DateTime timeTwo)
    {
        var response = new ApiResponse<DashboardAssistanceV2>();
        try
        {
            response.Result = _mapper.Map<DashboardAssistanceV2>(_dashboardRepository.GetAssistance(id, timeOne, timeTwo));
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

    /// <summary>
    /// GET:
    /// Return Performance by regional
    /// </summary>
    /// <returns></returns>
    [HttpGet("performance-regional/{city:int}/{branch:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<List<MermasDto>>> GetPerformanceRegional(int city, int branch, [FromQuery] DateTime initDate, [FromQuery] DateTime endDate)
    {
        var response = new ApiResponse<List<MermasDto>>();
        try
        {
            switch (city)
            {
                case (1):
                    //DB2
                    response.Result = _mapper.Map<List<MermasDto>>(
                        _stockDB2Repository.GetMermas(branch, initDate.AbsoluteStart(), endDate.AbsoluteEnd()));
                    break;
                case (2):
                    //DB1
                    response.Result = _mapper.Map<List<MermasDto>>(
                        _stockDB1Repository.GetMermas(branch, initDate.AbsoluteStart(), endDate.AbsoluteEnd()));
                    break;
            }

            response.Success = true;
            response.Message = "Operation was success";
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
    /// <summary>
    /// Method GET:
    /// performance general
    /// </summary>
    /// <param name="city">City</param>
    /// <param name="regional">Regional</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>Return a list with performance</returns>
    [HttpGet("performance-general/{city:int}/{regional:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<DashboardAdminPerformanceDto>> GetPerformanceGeneral(int city, int regional, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var response = new ApiResponse<DashboardAdminPerformanceDto>();
        try
        {
            var res = _mapper.Map<DashboardAdminPerformanceDto>(_dashboardRepository.GetAdminPerformance(city, regional, startDate.AbsoluteStart(), endDate.AbsoluteEnd()));
            switch (city)
            {
                case 1:
                    foreach (var performance in res.Performances)
                    {
                        if (_sucursalDB2Repository.getSucursalById(performance.IdBranch))
                        {
                            performance.NameBranch = _sucursalDB2Repository
                                .Find(x => x.Idfront == performance.IdBranch).Titulo;
                        }
                        else
                        {
                            performance.NameBranch = "La sucursal no existe";
                        }
                    }
                    break;
                case 2:
                    foreach (var performance in res.Performances)
                    {
                        if (_sucursalDB1Repository.getSucursalById(performance.IdBranch))
                        {
                            performance.NameBranch = _sucursalDB1Repository
                                .Find(x => x.Idfront == performance.IdBranch).Titulo;
                        }
                        else
                        {
                            performance.NameBranch = "La sucursal no existe";
                        }
                    }
                    break;
                default:
                    break;
            }
            if (res.Performances.Count > 0)
            {
                foreach (var i in res.Performances)
                {
                    res.Multi.Add(new BranchChartBarVerticalDto()
                    {
                        Name = i.NameBranch,
                        Series = new List<SerieDto>()
                    {
                        new(){ Name = "Completado", Value = i.Complete },
                        new(){ Name = "No Completado", Value = i.NoComplete },

                    }
                    });
                }
                response.Result = res;
                response.Message = "Operation was success";
                response.Success = true;
            }
            else {
                response.Result = res;
                response.Message = "SIN DATOS";
                response.Success = true;

            }
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
    
    /// <summary>
    /// Method GET:
    /// performance general supervisor
    /// </summary>
    /// <param name="city">City</param>
    /// <param name="regional">Regional</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>Return a list with performance</returns>
    [HttpGet("performance-general-supervisor/{city:int}/{regional:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<DashboardAdminPerformanceDto>> GetPerformanceGeneralSupervisor(int city, int regional, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var response = new ApiResponse<DashboardAdminPerformanceDto>();
        try
        {
            var res = _mapper.Map<DashboardAdminPerformanceDto>(_dashboardRepository.GetAdminPerformanceSupervisor(city, regional, startDate.AbsoluteStart(), endDate.AbsoluteEnd()));
            switch (city)
            {
                case 1:
                    foreach (var performance in res.Performances)
                    {
                        if (_sucursalDB2Repository.getSucursalById(performance.IdBranch))
                        {
                            performance.NameBranch = _sucursalDB2Repository
                                .Find(x => x.Idfront == performance.IdBranch).Titulo;
                        }
                        else
                        {
                            performance.NameBranch = "La sucursal no existe";
                        }
                    }
                    break;
                case 2:
                    foreach (var performance in res.Performances)
                    {
                        if (_sucursalDB1Repository.getSucursalById(performance.IdBranch))
                        {
                            performance.NameBranch = _sucursalDB1Repository
                                .Find(x => x.Idfront == performance.IdBranch).Titulo;
                        }
                        else
                        {
                            performance.NameBranch = "La sucursal no existe";
                        }
                    }
                    break;
                default:
                    break;
            }

            foreach (var i in res.Performances)
            {
                res.Multi.Add(new BranchChartBarVerticalDto()
                {
                    Name = i.NameBranch,
                    Series = new List<SerieDto>()
                    {
                        new(){ Name = "Completado", Value = i.Complete },
                        new(){ Name = "No Completado", Value = i.NoComplete },
                        
                    }
                });
                
            }

            var resultT = new List<RangosDto>();
            foreach (var i in res.Performances)
            {
                resultT = _mapper.Map<List<RangosDto>>(_tiemposRepository.GetGraficaTiempos(i.NameBranch, startDate.AbsoluteStart(), endDate.AbsoluteEnd()));

                res.Multi2.Add(new BranchChartBarVerticalDto()
                {
                    Name = i.NameBranch,
                    Series = new List<SerieDto>()
                    {

                        new(){ Name = resultT[0].nomRango, Value = resultT[0].RangoValor },
                        new(){ Name = resultT[1].nomRango, Value = resultT[1].RangoValor },
                        new(){ Name = resultT[2].nomRango, Value = resultT[2].RangoValor },

                    }
                });

            }

            var result25 = new List<sucAuditaDto>();
            foreach (var i in res.Performances)
            {
                result25 = _mapper.Map<List<sucAuditaDto>>(_i25ptsRepository.GetGrafica25pts(i.NameBranch, startDate.AbsoluteStart(), endDate.AbsoluteEnd()));

                res.TopOmittedTask2.Add(new TaskNoCompleteDto()
                {
                    Name = i.NameBranch, Value = result25[0].incidencias 

                });

            }


            response.Result = res;
            response.Message = "Operation was success";
            response.Success = true;
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

    /// <summary>
    /// GET:
    /// Return Performance by regional
    /// </summary>
    /// <returns></returns>
    [HttpGet("performance-sucursal-tiempos/{sucursal}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<List<TiemposDto>>> GetPerformanceSucursalTiempos(string sucursal, [FromQuery] DateTime initDate, [FromQuery] DateTime endDate)
    {
        var response = new ApiResponse<List<TiemposDto>>();
        try
        {
            
                   
            response.Result = _mapper.Map<List<TiemposDto>>(_tiemposRepository.GetTiempos(sucursal, initDate.AbsoluteStart(), endDate.AbsoluteEnd()));
                    
            

            response.Success = true;
            response.Message = "Operation was success";
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
    [HttpGet("performance-sucursal-25/{sucursal}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<List<_25ptsDto>>> GetPerformanceSucursal25(string sucursal, [FromQuery] DateTime initDate, [FromQuery] DateTime endDate)
    {
        var response = new ApiResponse<List<_25ptsDto>>();
        try
        {


            response.Result = _mapper.Map<List<_25ptsDto>>(_i25ptsRepository.Get25pts(sucursal, initDate.AbsoluteStart(), endDate.AbsoluteEnd()));



            response.Success = true;
            response.Message = "Operation was success";
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

    [HttpGet("performance-reporte/{city:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<List<ReporteDto>>> GetPerformanceReporte(int city, [FromQuery] DateTime initDate)
    {
        var response = new ApiResponse<List<ReporteDto>>();
        try
        {
            switch (city)
            {
                case (1):
                    //DB2
                    response.Result = _mapper.Map<List<ReporteDto>>(
                        _stockDB2Repository.GetReporte(initDate.AbsoluteStart()));
                    break;
                case (2):
                    //DB1
                    response.Result = _mapper.Map<List<ReporteDto>>(
                        _stockDB1Repository.GetReporte(initDate.AbsoluteStart()));
                    break;
            }

            response.Success = true;
            response.Message = "Operation was success";
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


    [HttpGet("performance-reporte-s/{city:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<List<ReporteDto>>> GetPerformanceReporteV(int city, [FromQuery] DateTime initDate)
    {
        var response = new ApiResponse<List<ReporteDto>>();
        try
        {
            switch (city)
            {
                case (1):
                    //DB2
                    response.Result = _mapper.Map<List<ReporteDto>>(
                        _stockDB2Repository.GetReporteV(initDate.AbsoluteStart()));
                    break;
                case (2):
                    //DB1
                    response.Result = _mapper.Map<List<ReporteDto>>(
                        _stockDB1Repository.GetReporteV(initDate.AbsoluteStart()));
                    break;
            }

            response.Success = true;
            response.Message = "Operation was success";
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



    [HttpGet("performance-venta-vendedor/{city:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<List<VendedorDto>>> GetPerformanceVentaVendedor(int city, [FromQuery] DateTime initDate, [FromQuery] DateTime endDate)
    {
        var response = new ApiResponse<List<VendedorDto>>();
        try
        {
            switch (city)
            {
                case (1):
                    //DB2
                    response.Result = _mapper.Map<List<VendedorDto>>(
                        _stockDB2Repository.GetVentaVendedor(initDate, endDate));
                    break;
                case (2):
                    //DB1
                    response.Result = _mapper.Map<List<VendedorDto>>(
                        _stockDB1Repository.GetVentaVendedor(initDate, endDate));
                    break;
            }

            response.Success = true;
            response.Message = "Operation was success";
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
    [HttpGet("performance-venta-vendedor-filtro/{city:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse<List<FiltroDto>>> GetPerformanceVentaVendedorFiltro(int city, [FromQuery] DateTime initDate, [FromQuery] DateTime endDate)
    {
        var response = new ApiResponse<List<FiltroDto>>();
        try
        {
            switch (city)
            {
                case (1):
                    //DB2
                    response.Result = _mapper.Map<List<FiltroDto>>(
                        _stockDB2Repository.GetVentaVendedorFiltro(initDate, endDate));
                    break;
                case (2):
                    //DB1
                    response.Result = _mapper.Map<List<FiltroDto>>(
                        _stockDB1Repository.GetVentaVendedorFiltro(initDate, endDate));
                    break;
            }

            response.Success = true;
            response.Message = "Operation was success";
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

    [HttpPost("envio_tiempos", Name = "envio_tiempos")]
    [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<List<EnvioTiemposDto>>>> envio_tiempos([FromBody] List<EnvioTiemposDto> envioTiemposDtos)
    {
        var response = new ApiResponse<List<EnvioTiemposDto>>();
        try
        {
            var orders = new List<EnvioTiemposDto>();
            foreach (var envioTiemposDto in envioTiemposDtos)
            {

                var order = await _tiemposRepository.AddAsyn(_mapper.Map<Tiempos>(envioTiemposDto));
                orders.Add(_mapper.Map<EnvioTiemposDto>(order));
            }

            response.Result = orders;
            response.Message = "Consult was success";
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            response.Success = false;
            response.Message = ex.ToString();
            return StatusCode(500, response);
        }
        return StatusCode(201, response);
    }
    [HttpPost("envio_25pts", Name = "envio_25pts")]
    [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<List<Envio25ptsDto>>>> envio_25pts([FromBody] List<Envio25ptsDto> envio25ptsDtos)
    {
        var response = new ApiResponse<List<Envio25ptsDto>>();
        try
        {
            var orders = new List<Envio25ptsDto>();
            foreach (var envio25ptsDto in envio25ptsDtos)
            {

                var order = await _i25ptsRepository.AddAsyn(_mapper.Map<_25pts>(envio25ptsDto));
                orders.Add(_mapper.Map<Envio25ptsDto>(order));
            }

            response.Result = orders;
            response.Message = "Consult was success";
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            response.Success = false;
            response.Message = ex.ToString();
            return StatusCode(500, response);
        }
        return StatusCode(201, response);
    }
}