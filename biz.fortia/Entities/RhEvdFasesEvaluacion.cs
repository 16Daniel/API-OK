﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhEvdFasesEvaluacion
    {
        public int ClaEmpresa { get; set; }
        public int ClaEvaluacion { get; set; }
        public int ClaFase { get; set; }
        public string NomFase { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? Estatus { get; set; }
        public DateTime? FechaUltCambio { get; set; }
    }
}