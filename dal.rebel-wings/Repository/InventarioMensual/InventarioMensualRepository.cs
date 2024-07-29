using biz.rebel_wings.Repository.InventarioMensual;
using dal.rebel_wings.DBContext;
using dal.rebel_wings.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Data;
using SpreadsheetLight;



namespace dal.rebel_wings.Repository.InventarioMensual;
public class InventarioMensualRepository : GenericRepository<biz.rebel_wings.Entities.InventarioMensual>, IInventarioMensualRepository
{
    public InventarioMensualRepository(Db_Rebel_WingsContext context) : base(context)
    {
    }

    public biz.rebel_wings.Models.InventarioMensual.InventarioMensual CreaCaptura(int city, int sucursal, int codarticulo, decimal? unidades, decimal? precio, decimal? stockant, string referencia, string medida, string descripcion, int registro, int? orden, string tipo)
    {

        var timeNow = DateTime.Now;
        biz.rebel_wings.Entities.InventarioMensual _registro = new biz.rebel_wings.Entities.InventarioMensual();
        biz.rebel_wings.Models.InventarioMensual.InventarioMensual __registro = new biz.rebel_wings.Models.InventarioMensual.InventarioMensual();
        // FECHA DE INVENTARIOS

        _registro.City = city;
        _registro.Sucursal = sucursal;
        _registro.Codarticulo = codarticulo;
        _registro.Unidades = unidades;
        _registro.StockAnterior = stockant;
        _registro.Referencia = referencia;
        _registro.Medida = medida;
        _registro.Diferencia = unidades - stockant;
        _registro.Valor = unidades * precio;
        _registro.Precio = precio;
        _registro.Procesado = false;
        _registro.Date = timeNow;
        _registro.Descripcion = descripcion;
        _registro.Registro = registro;
        _registro.orden = orden;
        _registro.tipo = tipo;


        _context.InventariosMensuales.Add(_registro);


        _context.SaveChanges();

        __registro.City = city;
        __registro.Sucursal = sucursal;
        __registro.Codarticulo = codarticulo;
        __registro.Unidades = unidades;
        __registro.StockAnterior = stockant;
        __registro.Referencia = referencia;
        __registro.Medida = medida;
        __registro.Diferencia = unidades - stockant;
        __registro.Valor = unidades * precio;
        __registro.Precio = precio;
        __registro.Procesado = false;
        __registro.Date = timeNow;
        __registro.Descripcion = descripcion;
        __registro.Registro = registro;
        __registro.orden = orden;
        __registro.tipo = tipo;
        return __registro;
    }

    public List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual> getCapturas(int registro)
    {
        List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual> _captura = new List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual>();

        _captura = _context.InventariosRegistrosMensuales
                    .Join(_context.InventariosMensuales,
                    art => art.Id,
                    stk => stk.Registro,
                    (art, stk) => new biz.rebel_wings.Models.InventarioMensual.InventarioMensual()
                    {
                        Id = stk.Id,
                        Registro = stk.Registro,
                        City = stk.City,
                        Sucursal = stk.Sucursal,
                        Codarticulo = stk.Codarticulo,
                        Referencia = stk.Referencia,
                        Descripcion = stk.Descripcion,
                        Medida = stk.Medida,
                        Unidades = stk.Unidades,
                        StockAnterior = stk.StockAnterior,
                        Diferencia = stk.Diferencia,
                        Valor = stk.Valor,
                        Precio = stk.Precio,
                        Date = stk.Date,
                        Procesado = stk.Procesado,
                        orden = stk.orden,
                        tipo = stk.tipo,

                    })
                    .Where(s => s.Registro == registro).ToList();

        return _captura;
    }

    public biz.rebel_wings.Models.InventarioMensual.InventarioMensual modificaCapturas(int idcaptura, decimal unidades)
    {
        biz.rebel_wings.Models.InventarioMensual.InventarioMensual _captura = new biz.rebel_wings.Models.InventarioMensual.InventarioMensual();

        var __captura = _context.InventariosMensuales.FirstOrDefault(x => x.Id == idcaptura);
        __captura.Unidades = unidades;
        __captura.Valor = unidades * __captura.Precio;
        __captura.Diferencia = unidades - __captura.StockAnterior;

        _context.InventariosMensuales.Update(__captura);
        _context.SaveChanges();

        _captura.Id = __captura.Id;
        _captura.Registro = __captura.Registro;
        _captura.Unidades = unidades;
        _captura.City = __captura.City;
        _captura.Sucursal = __captura.Sucursal;
        _captura.Codarticulo = __captura.Codarticulo;
        _captura.Referencia = __captura.Referencia;
        _captura.Descripcion = __captura.Descripcion;
        _captura.Medida = __captura.Medida;
        _captura.StockAnterior = __captura.StockAnterior;
        _captura.Diferencia = __captura.Diferencia;
        _captura.Valor = __captura.Valor;
        _captura.Precio = __captura.Precio;
        _captura.Procesado = __captura.Procesado;
        _captura.orden = __captura.orden;
        _captura.tipo = __captura.tipo;

        return _captura;
    }
    public Boolean procesadoCapturas(int idcaptura)
    {

        try
        {

            var __captura = _context.InventariosMensuales.FirstOrDefault(x => x.Id == idcaptura);
            __captura.Procesado = true;

            _context.InventariosMensuales.Update(__captura);
            _context.SaveChanges();


            return true;
        }
        catch { return false; }
    }
    public List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual> getCapturasExcel(int registro, string sucursal, string correo)
    {
        string pathFile = Environment.CurrentDirectory;
        string date = DateTime.UtcNow.ToString("ddMMyyyy");
        string ruta = @"\Files\Excel\"+sucursal+" "+registro+" "+date+".xlsx";
        string archivoRuta = pathFile + ruta;

        SLDocument oSLDocument = new SLDocument();
        System.Data.DataTable dt = new System.Data.DataTable();

        List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual> _captura = new List<biz.rebel_wings.Models.InventarioMensual.InventarioMensual>();
        string bodyinvA = "";
        string bodyinvB = "";
        string bodyinvC = "";
        string bodytotales = "";
        _captura = _context.InventariosRegistrosMensuales
                    .Join(_context.InventariosMensuales,
                    art => art.Id,
                    stk => stk.Registro,
                    (art, stk) => new biz.rebel_wings.Models.InventarioMensual.InventarioMensual()
                    {
                        Id = stk.Id,
                        Registro = stk.Registro,
                        City = stk.City,
                        Sucursal = stk.Sucursal,
                        Codarticulo = stk.Codarticulo,
                        Referencia = stk.Referencia,
                        Descripcion = stk.Descripcion,
                        Medida = stk.Medida,
                        Unidades = stk.Unidades,
                        StockAnterior = stk.StockAnterior,
                        Diferencia = stk.Diferencia,
                        Valor = stk.Valor,
                        Precio = stk.Precio,
                        Date = stk.Date,
                        Procesado = stk.Procesado,
                        orden = stk.orden,
                        tipo = stk.tipo,
                        ValorDif = (stk.Diferencia >= 0 ? 0 : ((stk.Diferencia * -1) * stk.Precio)),

                    })
                    .Where(s => s.Registro == registro).OrderBy(x => x.tipo).ToList();
        //columnas
        dt.Columns.Add("ORDEN", typeof(int));
        dt.Columns.Add("REFERENCIA", typeof(string));
        dt.Columns.Add("DESCRIPCION", typeof(string));
        dt.Columns.Add("PRECIO UNITARIO", typeof(decimal));
        dt.Columns.Add("TIPO", typeof(string));
        dt.Columns.Add("MEDIDA", typeof(string));
        dt.Columns.Add("CONTEO", typeof(decimal));
        dt.Columns.Add("SISTEMA", typeof(decimal));
        dt.Columns.Add("DIFERENCIA", typeof(decimal));
        dt.Columns.Add("VALOR INVENTARIO", typeof(decimal));
        dt.Columns.Add("VALOR FALTANTE", typeof(decimal));
        foreach (var row in _captura.OrderBy(x => x.orden))
        {

            dt.Rows.Add(row.orden, row.Referencia, row.Descripcion,row.Precio, row.tipo, row.Medida, row.Unidades, row.StockAnterior, row.Diferencia, row.Valor, row.ValorDif);
        }

        decimal? sumA = 0;
        decimal? sumAD = 0;
        decimal? sumB = 0;
        decimal? sumBD = 0;
        decimal? sumC = 0;
        decimal? sumCD = 0;
        foreach (var row in _captura.Where(x => x.tipo.ToString() == "ALIMENTO").OrderByDescending(n => n.ValorDif ))
        {
            bodyinvA += "<tr>";
            bodyinvA += "<td>" + row.Referencia + "</td>";
            bodyinvA += "<td>" + row.Descripcion + "</td>";
            bodyinvA += "<td>" + row.Precio + "</td>";
            bodyinvA += "<td>" + row.tipo + "</td>";
            bodyinvA += "<td>" + row.Medida + "</td>";
            bodyinvA += "<td>" + row.Unidades + "</td>";
            bodyinvA += "<td>" + row.StockAnterior + "</td>";
            bodyinvA += "<td>" + row.Diferencia + "</td>";
            bodyinvA += "<td>" + row.Valor + "</td>";
            bodyinvA += "<td>" + ((float)row.ValorDif.Value) +"</td>";
            bodyinvA += "</tr>";
            sumA += row.Valor;
            sumAD += row.ValorDif;

        }
        foreach (var row in _captura.Where(x => x.tipo.ToString() == "BEBIDA").OrderByDescending(n => n.ValorDif))
        {
            bodyinvB += "<tr>";
            bodyinvB += "<td>" + row.Referencia + "</td>";
            bodyinvB += "<td>" + row.Descripcion + "</td>";
            bodyinvB += "<td>" + row.Precio + "</td>";
            bodyinvB += "<td>" + row.tipo + "</td>";
            bodyinvB += "<td>" + row.Medida + "</td>";
            bodyinvB += "<td>" + row.Unidades + "</td>";
            bodyinvB += "<td>" + row.StockAnterior + "</td>";
            bodyinvB += "<td>" + row.Diferencia + "</td>";
            bodyinvB += "<td>" + row.Valor + "</td>";
            bodyinvB += "<td>" + ((float)row.ValorDif.Value) + "</td>";
            bodyinvB += "</tr>";
            sumB += row.Valor;
            sumBD += row.ValorDif;
        }
        foreach (var row in _captura.Where(x => x.tipo.ToString() == "CONSUMIBLES").OrderByDescending(n => n.ValorDif))
        {
            bodyinvC += "<tr>";
            bodyinvC += "<td>" + row.Referencia + "</td>";
            bodyinvC += "<td>" + row.Descripcion + "</td>";
            bodyinvC += "<td>" + row.Precio + "</td>";
            bodyinvC += "<td>" + row.tipo + "</td>";
            bodyinvC += "<td>" + row.Medida + "</td>";
            bodyinvC += "<td>" + row.Unidades + "</td>";
            bodyinvC += "<td>" + row.StockAnterior + "</td>";
            bodyinvC += "<td>" + row.Diferencia + "</td>";
            bodyinvC += "<td>" + row.Valor + "</td>";
            bodyinvC += "<td>" + ((float)row.ValorDif.Value) + "</td>";
            bodyinvC += "</tr>";
            sumC += row.Valor;
            sumCD += row.ValorDif;
        }


        bodytotales += "<tr>";
        bodytotales += "<td>ALIMENTOS</td>";
        bodytotales += "<td>" + ((float)sumA) + "</td>";
        bodytotales += "<td>" + ((float)sumAD) + "</td>";
        bodytotales += "</tr>";
        bodytotales += "<tr>";
        bodytotales += "<td>BEBIDAS</td>";
        bodytotales += "<td>" + ((float)sumB) + "</td>";
        bodytotales += "<td>" + ((float)sumBD) + "</td>";
        bodytotales += "</tr>";
        bodytotales += "<tr>";
        bodytotales += "<td>CONSUMIBLES</td>";
        bodytotales += "<td>" + ((float)sumC) + "</td>";
        bodytotales += "<td>" + ((float)sumCD) + "</td>";
        bodytotales += "</tr>";

        oSLDocument.ImportDataTable(1, 1, dt, true);
        oSLDocument.RenameWorksheet(SLDocument.DefaultFirstSheetName, sucursal);
        oSLDocument.SaveAs(archivoRuta);

        string bodymail = getBody(bodytotales,bodyinvA , bodyinvB, bodyinvC, sucursal);
        EnviarCorreo(bodymail, archivoRuta, correo, sucursal);
        return _captura;
    }
    static void EnviarCorreo(string bodymail, string archivo, string correo, string sucursal)
    {
        // Configurar la información de la cuenta de Gmail
        //string correoRemitente = "gilberto.r@operamx.com";
        //string contraseña = "Gil19315";
        string date = DateTime.UtcNow.ToString("dd-MM-yyyy");
        // Configurar la información de la cuenta de Gmail
        string correoRemitente = "gilberto.r@operamx.com";
        string contraseña = "GRC1931519315";

        // Configurar la información del destinatario
        // string correoDestinatario = "developeramh@outlook.com";
        string correoDestinatario = correo;
        //string correoDestinatario = "daniel.h@operamx.com";
        string asunto = "📦 INVENTARIO MENSUAL " + sucursal+" "+date;

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
        mensaje.Attachments.Add(new Attachment(archivo));

        ////direccion
        mensaje.To.Add("enrique.j@operamx.com");
        mensaje.To.Add("jorge.j@operamx.com");
        mensaje.To.Add("adrian.c@operamx.com");
        mensaje.To.Add("gilberto.r@operamx.com");
        mensaje.To.Add("roberto.c@operamx.com");
        mensaje.To.Add("carlos.c@operamx.com");

        ////servicio
        mensaje.To.Add("jose.r@operamx.com");
        mensaje.To.Add("eduardo.p@operamx.com");
        mensaje.To.Add("christopher.m@operamx.com");
        mensaje.To.Add("monica.r@operamx.com");
        mensaje.To.Add("ricardo.g@operamx.com");
        mensaje.To.Add("sergio.g@operamx.com");
        mensaje.To.Add("daniel.h@operamx.com");

        // mensaje.To.Add("arturo.m@operamx.com");

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

    public string getBody(string bodytotales, string bodyinvA, string bodyinvB, string bodyinvC, string sucursal)
    {
        string date = DateTime.UtcNow.ToString("dd/MM/yyyy");
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
                  max-width: 900px;
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
                  text-align: center;
                  font-size: 70%;
                }

              </style>
            </head>
            <body>
              <div class=""container"">
                <h1 style=""color: rgb(255, 166, 0); text-align: center;"">*--fecha*</h1>
                <h1 style=""color: rgb(255, 166, 0); text-align: center;"">*📦 INVENTARIO --sucursal*</h1>

                <p></p>
                
                <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">TOTALES</H4>
                <table align=""center"">
                  <thead>
                      <tr>
                          <th style=""background-color: rgb(255, 206, 1);"">TIPO</th>
                          <th style=""background-color: rgb(2, 132, 199);color: rgb(255, 255, 255)"">TOTAL INVENTARIO</th>     
                          <th style=""background-color: rgb(227, 66, 66);color: rgb(255, 255, 255)"">TOTAL FALTANTE</th>   
                      </tr>         
                  </thead>
                  <tbody>
                    --bodytotales
                  </tbody>
                </table>

                <p></p>

                
                <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">ALIMENTOS </H4>
                <table align=""center"">
                  <thead style=""background-color: rgb(255, 230, 0);"">
                      <tr>
                          <th>REFERENCIA</th>
                          <th>DESCRIPCION</th>     
                          <th>PRECIO U.</th>    
                          <th>TIPO</th>   
                          <th>MEDIDA</th>
                          <th>CONTEO</th>
                          <th>SISTEMA</th>
                          <th>DIFERENCIA</th>
                          <th>VALOR INVENTARIO</th>
                          <th>VALOR FALTANTE</th>
                      </tr>         
                  </thead>
                  <tbody>
                    --bodyinvA
                  </tbody>
                </table>
                <p></p>
                
                <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">BEBIDAS </H4>
                <table align=""center"">
                  <thead style=""background-color: rgb(255, 230, 0);"">
                      <tr>
                          <th>REFERENCIA</th>
                          <th>DESCRIPCION</th>     
                          <th>PRECIO U.</th> 
                          <th>TIPO</th>   
                          <th>MEDIDA</th>
                          <th>CONTEO</th>
                          <th>SISTEMA</th>
                          <th>DIFERENCIA</th>
                          <th>VALOR INVENTARIO</th>
                          <th>VALOR FALTANTE</th>
                      </tr>         
                  </thead>
                  <tbody>
                    --bodyinvB
                  </tbody>
                </table>
                <p></p>
                
                <H4 style=""background-color: #ddd; padding: 5px; text-align: center; border-radius: 5px;"">CONSUMIBLES </H4>
                <table align=""center"">
                  <thead style=""background-color: rgb(255, 230, 0);"">
                      <tr>
                          <th>REFERENCIA</th>
                          <th>DESCRIPCION</th>   
                          <th>PRECIO U.</th> 
                          <th>TIPO</th>   
                          <th>MEDIDA</th>
                          <th>CONTEO</th>
                          <th>SISTEMA</th>
                          <th>DIFERENCIA</th>
                          <th>VALOR INVENTARIO</th>
                          <th>VALOR FALTANTE</th>
                      </tr>         
                  </thead>
                  <tbody>
                    --bodyinvC
                  </tbody>
                </table>



              </div>
            </body>
            </html>";
        template = template.Replace("--bodytotales", bodytotales);
        template = template.Replace("--bodyinvA", bodyinvA);
        template = template.Replace("--bodyinvB", bodyinvB);
        template = template.Replace("--bodyinvC", bodyinvC);
        template = template.Replace("--sucursal", sucursal);
        template = template.Replace("--fecha", date);
        return template;
    }
}
