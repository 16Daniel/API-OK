﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class SysRepPersCfdi
    {
        public int ClaEmpresa { get; set; }
        public int ClaReporte { get; set; }
        public string Reporte { get; set; }
        public string ClaPantalla { get; set; }
        public string Procedimiento { get; set; }
        public string NomReporte { get; set; }
        public int Tipo { get; set; }
        public string SubReporte { get; set; }
        public string SubReporteIncap { get; set; }
        public string SubReporteTe { get; set; }
        public string SubReporteConcepto { get; set; }
        public int? Modalidad { get; set; }
        public byte[] Contenido { get; set; }
        public DateTime? FechaUltCambioContenido { get; set; }
    }
}