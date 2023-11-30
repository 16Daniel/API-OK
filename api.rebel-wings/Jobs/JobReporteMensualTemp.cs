using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using dal.rebel_wings.DBContext;
using NCrontab;
using biz.rebel_wings.Repository.Dashboard;
using api.rebel_wings.Models.ApiResponse;
using api.rebel_wings.Models.Dashboard;
using api.rebel_wings.Models.Implementacion;
using AutoMapper;
using biz.rebel_wings.Entities;
using biz.rebel_wings.Services.Logger;
using Newtonsoft.Json;
using System.Web.WebPages;
using biz.rebel_wings.Repository.User;
using api.rebel_wings.Models.User;
using api.rebel_wings.Models.Mermas;

namespace api.rebel_wings.Jobs
{
    public class JobReporteMensualTemp : BackgroundService
    {
        public Db_Rebel_WingsContext _context;
        private readonly IServiceScopeFactory _scopeFactory;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        public ILoggerManager _logger;
        public IMapper _mapper;
        public IDashboardRepository _dashboardRepository;
        public biz.bd1.Repository.Sucursal.ISucursalRepository _sucursalDB1Repository;
        public biz.bd2.Repository.Sucursal.ISucursalRepository _sucursalDB2Repository;
        public biz.rebel_wings.Repository.Implementacion.ITiemposRepository _tiemposRepository;
        public biz.rebel_wings.Repository.Implementacion.I25ptsRepository _i25ptsRepository;
        public IUserRepository _userRepository;

        //private string Schedule => "0 3 1 * *";
        private string Schedule => "0 3 * * *";

        public JobReporteMensualTemp(IServiceScopeFactory serviceScopeFactory) 
        {
            _scopeFactory = serviceScopeFactory;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await System.Threading.Tasks.Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            string jsondata = string.Empty;
        
            // Obtener fechas del primer día del mes anterior y el último día del mes
            DateTime now = DateTime.Now;
            now.AddDays(-1);
            DateTime primerDiaMesAnterior;
            DateTime ultimoDiaMesAnterior;

            UserDto all = new UserDto();
            all.Id = -1;
            all.Name = "TODOS";
            List<UserDto> regionalesQro = getRegionales(2);
            regionalesQro.Add(all);
            List<UserDto> regionalesmx = getRegionales(1);
            regionalesmx.Add(all);  

                    primerDiaMesAnterior = new DateTime(now.Year, now.Month, 1);
                    ultimoDiaMesAnterior = new DateTime(now.Year, now.Month, now.Day);

                    foreach (UserDto regional in regionalesQro)
                    {
                        var reporte = obtenerReporte(2, regional.Id, primerDiaMesAnterior, ultimoDiaMesAnterior);
                        if (reporte == null)
                        {

                            var response = genrarReporte(1, -1, primerDiaMesAnterior, ultimoDiaMesAnterior);
                            jsondata = JsonConvert.SerializeObject(response);

                            try
                            {
                                using IServiceScope scope2 = _scopeFactory.CreateScope();
                                _context = scope2.ServiceProvider.GetService<Db_Rebel_WingsContext>();
                                SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
                                SqlCommand cmd = connection.CreateCommand();
                                connection.Open();
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.CommandText = "insertar_reporte_mensual_temp";
                                cmd.Parameters.Add("@city", System.Data.SqlDbType.Int).Value = 2;
                                cmd.Parameters.Add("@regional", System.Data.SqlDbType.Int).Value = regional.Id;
                                cmd.Parameters.Add("@startdate", System.Data.SqlDbType.DateTime).Value = primerDiaMesAnterior;
                                cmd.Parameters.Add("@enddate", System.Data.SqlDbType.DateTime).Value = ultimoDiaMesAnterior;
                                cmd.Parameters.Add("@json", System.Data.SqlDbType.NVarChar).Value = jsondata;
                                cmd.CommandTimeout = 120;
                                cmd.ExecuteNonQuery();
                                connection.Close();
                            }
                            catch (Exception ex)
                            { }
                        }
                    }

                    foreach (UserDto regional in regionalesmx)
                    {
                        var reporte = obtenerReporte(1, -1, primerDiaMesAnterior, ultimoDiaMesAnterior);
                        if (reporte == null)
                        {

                            var response = genrarReporte(1, regional.Id, primerDiaMesAnterior, ultimoDiaMesAnterior);
                            jsondata = JsonConvert.SerializeObject(response);

                            try
                            {
                                using IServiceScope scope2 = _scopeFactory.CreateScope();
                                _context = scope2.ServiceProvider.GetService<Db_Rebel_WingsContext>();
                                SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
                                SqlCommand cmd = connection.CreateCommand();
                                connection.Open();
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.CommandText = "insertar_reporte_mensual_temp";
                                cmd.Parameters.Add("@city", System.Data.SqlDbType.Int).Value = 1;
                                cmd.Parameters.Add("@regional", System.Data.SqlDbType.Int).Value = regional.Id;
                                cmd.Parameters.Add("@startdate", System.Data.SqlDbType.DateTime).Value = primerDiaMesAnterior;
                                cmd.Parameters.Add("@enddate", System.Data.SqlDbType.DateTime).Value = ultimoDiaMesAnterior;
                                cmd.Parameters.Add("@json", System.Data.SqlDbType.NVarChar).Value =jsondata;
                                cmd.CommandTimeout = 120;
                                cmd.ExecuteNonQuery();                           
                            }
                            catch (Exception ex)
                            { }
                        }
                    }

        }

        public ApiResponse<DashboardAdminPerformanceDto> genrarReporte(int city, int regional, DateTime startDate, DateTime endDate) 
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            _logger = scope.ServiceProvider.GetService<ILoggerManager>();
            _mapper = scope.ServiceProvider.GetService<IMapper>();
            _dashboardRepository = scope.ServiceProvider.GetService<IDashboardRepository>();
            _sucursalDB1Repository = scope.ServiceProvider.GetService<biz.bd1.Repository.Sucursal.ISucursalRepository>();
            _sucursalDB2Repository = scope.ServiceProvider.GetService<biz.bd2.Repository.Sucursal.ISucursalRepository>();
            _tiemposRepository = scope.ServiceProvider.GetService<biz.rebel_wings.Repository.Implementacion.ITiemposRepository>();
            _i25ptsRepository = scope.ServiceProvider.GetService<biz.rebel_wings.Repository.Implementacion.I25ptsRepository>();

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
                        Name = i.NameBranch,
                        Value = result25[0].incidencias

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
                return null;
            }

            return response;
        }

        public ApiResponse<DashboardAdminPerformanceDto> obtenerReporte(int city, int regional, DateTime startDate, DateTime endDate) 
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            _context = scope.ServiceProvider.GetService<Db_Rebel_WingsContext>();
            var response = new ApiResponse<DashboardAdminPerformanceDto>();
            string json = "";
            try
            {
                SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = connection.CreateCommand();
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "obtener_reporte_mensual_temp";
                cmd.Parameters.Add("@city", System.Data.SqlDbType.Int).Value = city;
                cmd.Parameters.Add("@regional", System.Data.SqlDbType.Int).Value = regional;
                cmd.Parameters.Add("@startdate", System.Data.SqlDbType.DateTime).Value = startDate;
                cmd.Parameters.Add("@enddate", System.Data.SqlDbType.DateTime).Value = endDate;
                cmd.CommandTimeout = 120;
                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    json = data["json"].ToString();   
                }
                connection.Close(); 
                response = JsonConvert.DeserializeObject<ApiResponse<DashboardAdminPerformanceDto>>(json);
                return response;
            }
            catch (Exception ex)
            { return null; }
           
        }

        public List<UserDto> getRegionales(int city) 
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            _mapper = scope.ServiceProvider.GetService<IMapper>();
            _userRepository = scope.ServiceProvider.GetService<IUserRepository>();

            return _mapper.Map<List<UserDto>>(_userRepository.FindAll(f => f.RoleId.Equals(2) && f.StateId == city));
        }
    }
}
