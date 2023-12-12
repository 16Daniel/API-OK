using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using dal.rebel_wings.DBContext;
using NCrontab;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace api.rebel_wings.Jobs
{
    public class JobUltimaActualizacion : BackgroundService
    {
        public Db_Rebel_WingsContext _context;
        private readonly IServiceScopeFactory _scopeFactory;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private string Schedule => "0 7 * * *";

        public JobUltimaActualizacion(IServiceScopeFactory serviceScopeFactory) 
        {
            _scopeFactory = serviceScopeFactory;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
                await Task.Delay(60000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            string bodytec = "";
            string body25p = ""; 
            try
            {
                string storedProc = "obtener_actualizaciones";
                using IServiceScope scope = _scopeFactory.CreateScope();
                _context = scope.ServiceProvider.GetService<Db_Rebel_WingsContext>();

                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comm;
                conn.Open();
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandTimeout = 120;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = storedProc;
                comm.Parameters.Clear();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataSet dt = new DataSet();
                sqlDataAdapter.Fill(dt, storedProc);
                comm.Dispose();
                conn.Close();
                conn.Dispose();

                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    bodytec += "<tr>";
                    bodytec += "<td>" + row[0] +"</td>";
                    bodytec += "<td>" + row[1] + "</td>";
                    bodytec += "</tr>";
                }

                foreach (DataRow row in dt.Tables[1].Rows)
                {
                    body25p += "<tr>";
                    body25p += "<td>" + row[0] + "</td>";
                    body25p += "<td>" + row[1] + "</td>";
                    body25p += "</tr>";
                }
                string bodymail = getBody(bodytec,body25p);
                EnviarCorreo(bodymail);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex2)
            {
                throw new Exception(ex2.Message);
            }

        }

        public string getBody(string bodytec, string body25p)
        {
            string template = @"<!DOCTYPE html>
<html lang=""es"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>Email</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
      background-color: #eee;
    }

    .container {
    width: 90%;
      max-width: 600px;
      margin: 0 auto;
      padding: 20px;
      background-color: white;
      border-radius: 10px;
      margin-top: 20px;
      margin-bottom: 20px;
    }

    p {
      color: #555;
      text-align: center;
    }

    table {
      border-collapse: collapse;
      width: 100%;
    }
    th, td {
      border: 1px solid rgb(134, 134, 134);
      padding: 5px;
      text-align: left;
    }

  </style>
</head>
<body>
  <div class=""container"">
    <h1 style=""color: rgb(255, 166, 0); text-align: center;"">*W A R N I N G*</h1>
    <p>REVISA TIEMPOS EN COCINA Y 25 PUNTOS DE LAS SIGUIENTES SUCURSALES</p>

    <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">TIMEPOS EN COCINA</H4>
    <table align=""center"">
      <thead style=""background-color: rgb(255, 230, 0);"">
          <tr>
              <th>SUCURSAL</th>
              <th>ÚLTIMA ACTUALIZACIÓN</th>
            </tr>         
      </thead>
      <tbody>
        --bodytec
      </tbody>
    </table>

<H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">25 PUNTOS</H4>
    <table align=""center"">
        <thead style=""background-color: rgb(255, 230, 0);"">
            <tr>
                <th>SUCURSAL</th>
                <th>ÚLTIMA ACTUALIZACIÓN</th>
              </tr>         
        </thead>
        <tbody>
            --body25p
        </tbody>
      </table>
  </div>
</body>
</html>";

            template = template.Replace("--bodytec", bodytec);
            template = template.Replace("--body25p", body25p);
            return template;
        }

        static void EnviarCorreo(string bodymail)
        {
            // Configurar la información de la cuenta de Gmail
            string correoRemitente = "arturo.m@operamx.com";
            string contraseña = "C-Opera-AMH151298"; 

            // Configurar la información del destinatario
           // string correoDestinatario = "developeramh@outlook.com";
            string correoDestinatario = "arturo.m@operamx.com";
            string asunto = "POLEO TIEMPOS EN COCINA Y 25 PUNTOS";

            // Configurar el cliente SMTP de Gmail
            SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(correoRemitente, contraseña),
                EnableSsl = true,
            };

            // Crear el mensaje de correo
            MailMessage mensaje = new MailMessage(correoRemitente, correoDestinatario, asunto, string.Empty)
            {
                IsBodyHtml = true,
                Body = bodymail,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
            };

            mensaje.To.Add("eduardo.o@operamx.com");
            mensaje.To.Add("daniel.f@operamx.com");
            mensaje.To.Add("daniel.h@operamx.com");
            mensaje.To.Add("gilberto.r@operamx.com");
            mensaje.To.Add("melecio.a@operamx.com");
            mensaje.To.Add("alejandro.l@operamx.com");
            mensaje.To.Add("francisco.q@operamx.com");

            try
            {
                // Enviar el mensaje
                clienteSmtp.Send(mensaje);
                Console.WriteLine("Correo enviado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
            finally
            {
                // Liberar recursos
                mensaje.Dispose();
            }
        }

    }
}
