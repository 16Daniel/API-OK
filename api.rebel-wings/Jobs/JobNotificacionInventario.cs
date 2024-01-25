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
using dal.bd2.DBContext;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.rebel_wings.Jobs
{
    public class JobNotificacionInventario : BackgroundService
    {
        protected BD2Context _contextdb1;
        private readonly IServiceScopeFactory _scopeFactory;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private string Schedule => "00 12 * * *";

        public JobNotificacionInventario(IServiceScopeFactory serviceScopeFactory)
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
                await Task.Delay(1000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        public async void Process()
        {
            string bodytab1 = "";
            string bodytab2 = "";
            string bodytab3 = "";
            string bodytab4 = "";
            string bodytab5 = "";
            Boolean enviaremail = false;
            try
            {
                string storedProc = "SP_MAT_VES_REPORTE";
                using IServiceScope scope = _scopeFactory.CreateScope();
                _contextdb1 = scope.ServiceProvider.GetService<BD2Context>();

                SqlConnection conn = (SqlConnection)_contextdb1.Database.GetDbConnection();
                SqlCommand comm;
                conn.Open();
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandTimeout = 120;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = storedProc;
                //comm.Parameters.Add("@FECHA", System.Data.SqlDbType.VarChar, 10).Value = new DateTime(2024,1,12,10,10,0).ToString("dd/MM/yyyy");
                comm.Parameters.Add("@FECHA", System.Data.SqlDbType.VarChar, 10).Value = DateTime.Now.ToString("dd/MM/yyyy");
                //comm.Parameters.Add("@HORAMAXIMA", System.Data.SqlDbType.VarChar, 50).Value = new DateTime(2024, 1, 12, 10, 00, 59).ToString("dd/MM/yyyy HH:mm:ss");
                comm.Parameters.Add("@HORAMAXIMA", System.Data.SqlDbType.VarChar, 50).Value = new DateTime(2024, 1, 12, 11, 00, 59).ToString("dd/MM/yyyy HH:mm:ss");
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataSet dt = new DataSet();
                sqlDataAdapter.Fill(dt, storedProc);
                comm.Dispose();
                conn.Close();
                conn.Dispose();

                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    if (dt.Tables[0].Rows.Count > 0) { enviaremail = true; }
                    bodytab1 += "<tr>";
                    bodytab1 += "<td>" + row[1] + "</td>";
                    bodytab1 += "<td>" + row[2] + "</td>";
                    bodytab1 += "<td>" + row[3] + "</td>";
                    bodytab1 += "<td>" + row[4] + "</td>";
                    bodytab1 += "</tr>";
                }

                foreach (DataRow row in dt.Tables[1].Rows)
                {
                    if (dt.Tables[1].Rows.Count > 0) { enviaremail = true; }
                    bodytab2 += "<tr>";
                    bodytab2 += "<td>" + row[1] + "</td>";
                    bodytab2 += "<td>" + row[2] + "</td>";
                    bodytab2 += "<td>" + row[3] + "</td>";
                    bodytab2 += "<td>" + row[4] + "</td>";
                    bodytab2 += "</tr>";
                }

                foreach (DataRow row in dt.Tables[2].Rows)
                {
                    if (dt.Tables[2].Rows.Count > 0) { enviaremail = true; }
                    bodytab3 += "<tr>";
                    bodytab3 += "<td>" + row[1] + "</td>";
                    bodytab3 += "<td>" + row[2] + "</td>";
                    bodytab3 += "<td>" + row[3] + "</td>";
                    bodytab3 += "<td>" + row[4] + "</td>";
                    bodytab3 += "<td>" + row[5] + "</td>";
                    bodytab3 += "<td>" + row[6].ToString().Substring(11, 8) + "</td>";
                    bodytab3 += "</tr>";
                }

                foreach (DataRow row in dt.Tables[3].Rows)
                {
                    if (dt.Tables[3].Rows.Count > 0) { enviaremail = true; }
                    bodytab4 += "<tr>";
                    bodytab4 += "<td>" + row[1] + "</td>";
                    bodytab4 += "<td>" + row[2] + "</td>";
                    bodytab4 += "<td>" + row[3] + "</td>";
                    bodytab4 += "<td>" + row[4] + "</td>";
                    bodytab4 += "</tr>";
                }

                foreach (DataRow row in dt.Tables[4].Rows)
                {
                    if (dt.Tables[4].Rows.Count > 0) { enviaremail = true; }
                    bodytab5 += "<tr>";
                    bodytab5 += "<td>" + row[1] + "</td>";
                    bodytab5 += "<td>" + row[2] + "</td>";
                    bodytab5 += "<td>" + row[3] + "</td>";
                    bodytab5 += "<td>" + row[4] + "</td>";
                    bodytab5 += "</tr>";
                }


                string bodymail = getBody(bodytab1, bodytab2, bodytab3,bodytab4, bodytab5);
                if (enviaremail) { EnviarCorreo(bodymail); } 
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex2)
            {
                // Configurar la información de la cuenta de Gmail
                string correoRemitente = "it_token@operamx.com";
                string contraseña = "M@5TERKEY";

                // Configurar la información del destinatario
                // string correoDestinatario = "developeramh@outlook.com";
                string correoDestinatario = "arturo.m@operamx.com";
                string asunto = "Error al enviar correo de notificacion";

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
                    Body = "Error al enviar el correo de notificación: " + ex2.Message,
                    SubjectEncoding = Encoding.UTF8,
                    BodyEncoding = Encoding.UTF8
                };

                clienteSmtp.Send(mensaje);
            }

        }

        public string getBody(string bodytab1,string bodytab2,string bodytab3, string bodytab4, string bodytab5)
        {
            string template = @"<!DOCTYPE html>
<html lang=""es"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>AVISO CARGA DE INVENTARIO</title>
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

    <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">SUCURSALES SIN CAPTURA DE INVENTARIO O CON CAPTURA PARCIAL TURNO MATUTINO</H4>
    <table align=""center"">
      <thead style=""background-color: rgb(255, 230, 0);"">
          <tr>
            <th>REGIÓN</th>
              <th>SUCURSAL</th>
              <th>TIPO CAPTURA</th>
              <th>TOTAL ARTÍCULOS SIN CAPTURA</th>
            </tr>         
      </thead>
      <tbody>
        --bodytab1
      </tbody>
    </table>

    <h4 style=""background-color:#ddd;padding:5px;text-align:center;border-radius:5px"">ARTÍCULOS SIN CAPTURA DE INVENTARIO TURNO MATUTINO</h4>
    <table align=""center"">
      <thead style=""background-color:rgb(255,230,0)"">
          <tr>
            <th>REGIÓN</th>
            <th>SUCURSAL</th>
            <th>ARTÍCULO</th>
            <th>SECCIÓN</th>
            </tr>         
      </thead>
      <tbody>
        --bodytab2
      </tbody>
    </table>


    <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">ARTÍCULOS CAPTURADOS DESPUÉS DE LA HORA MÁXIMA TURNO MATUTINO</H4>
    <table align=""center"">
      <thead style=""background-color: rgb(255, 230, 0);"">
        <tr>
          <th>REGIÓN</th>
          <th>SUCURSAL</th>
          <th>ARTÍCULO</th>
          <th>SECCIÓN</th>
          <th>INVENTARIO</th>
          <th>CAPTURA</th>
          </tr>      
      </thead>
      <tbody>
        --bodytab3
      </tbody>
    </table>

    <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">SUCURSALES SIN CAPTURA DE INVENTARIO O CON CAPTURA PARCIAL TURNO VESPERTINO</H4>
    <table align=""center"">
      <thead style=""background-color: rgb(255, 230, 0);"">
          <tr>
            <th>REGIÓN</th>
              <th>SUCURSAL</th>
              <th>TIPO CAPTURA</th>
              <th>TOTAL ARTÍCULOS SIN CAPTURA</th>
            </tr>         
      </thead>
      <tbody>
        --bodytab4
      </tbody>
    </table>

    <h4 style=""background-color:#ddd;padding:5px;text-align:center;border-radius:5px"">ARTÍCULOS SIN CAPTURA DE INVENTARIO TURNO VESPERTINO</h4>
    <table align=""center"">
      <thead style=""background-color:rgb(255,230,0)"">
          <tr>
            <th>REGIÓN</th>
            <th>SUCURSAL</th>
            <th>ARTÍCULO</th>
            <th>SECCIÓN</th>
            </tr>         
      </thead>
      <tbody>
        --bodytab5
      </tbody>
    </table>

  </div>
</body>
</html>";

            template = template.Replace("--bodytab1", bodytab1);
            template = template.Replace("--bodytab2", bodytab2);
            template = template.Replace("--bodytab3", bodytab3);
            template = template.Replace("--bodytab4", bodytab4);
            template = template.Replace("--bodytab5", bodytab5);
            return template;
        }

        static void EnviarCorreo(string bodymail)
        {
            //// Configurar la información de la cuenta de Gmail
            //string correoRemitente = "gilberto.r@operamx.com";
            //string contraseña = "Gil19315";

            // Configurar la información de la cuenta de Gmail
            string correoRemitente = "it_token@operamx.com";
            string contraseña = "M@5TERKEY";

            // Configurar la información del destinatario
            // string correoDestinatario = "developeramh@outlook.com";
            string correoDestinatario = "arturo.m@operamx.com";
            string asunto = "NOTIFICACIÓN CARGA DE INVENTARIOS";

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

            //mensaje.To.Add("jose.r@operamx.com");
            //mensaje.To.Add("eduardo.p@operamx.com");
            //mensaje.To.Add("christopher.m@operamx.com");
            //mensaje.To.Add("monica.r@operamx.com");
            //mensaje.To.Add("ricardo.g@operamx.com");

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
