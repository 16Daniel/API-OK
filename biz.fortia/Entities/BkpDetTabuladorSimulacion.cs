﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class BkpDetTabuladorSimulacion
    {
        public int ClaSimulacion { get; set; }
        public int ClaEmpresa { get; set; }
        public int ClaTabulador { get; set; }
        public int Nivel { get; set; }
        public string Descripcion { get; set; }
        public int PtosMinimos { get; set; }
        public int PtosMaximos { get; set; }
        public decimal Minimo { get; set; }
        public decimal? Media { get; set; }
        public decimal? Maximo { get; set; }
        public DateTime? FechaUltCambio { get; set; }
    }
}