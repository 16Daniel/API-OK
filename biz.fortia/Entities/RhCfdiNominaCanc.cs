﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhCfdiNominaCanc
    {
        public int IdCfdi { get; set; }
        public int? IdProceso { get; set; }
        public int? NumSesion { get; set; }
        public string RefGuid { get; set; }
        public int ClaEmpresa { get; set; }
        public int ClaPeriodo { get; set; }
        public int NumNomina { get; set; }
        public int ClaTrab { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public string Version { get; set; }
        public DateTime? FechaInicialPago { get; set; }
        public DateTime? FechaFinalPago { get; set; }
        public double? NumDiasPagados { get; set; }
        public double? TotalGravadoPer { get; set; }
        public double? TotalExentoPer { get; set; }
        public double? TotalGravadoDed { get; set; }
        public double? TotalExentoDed { get; set; }
    }
}