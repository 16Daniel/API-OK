﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhIndicadoresObjetivo
    {
        public int FolObjetivo { get; set; }
        public int ClaIndicador { get; set; }
        public string NomIndicador { get; set; }
        public int ClaTipoEscala { get; set; }
        public double? Ponderacion { get; set; }
        public double? Calificacion { get; set; }
        public DateTime FechaUltCambio { get; set; }
        public double Meta { get; set; }
        public string Unidad { get; set; }
        public string Evidencia { get; set; }
        public int? TipoIndicador { get; set; }
    }
}