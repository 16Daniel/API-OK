﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhBtEmpleoAnt
    {
        public int? ClaTrab { get; set; }
        public int FolAuto { get; set; }
        public DateTime FechaIng { get; set; }
        public DateTime? FechaSep { get; set; }
        public string Empresa { get; set; }
        public string Giro { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string PuestoIni { get; set; }
        public double? SueldoIni { get; set; }
        public string PuestoFin { get; set; }
        public double? SueldoFin { get; set; }
        public int? PersACargo { get; set; }
        public string UltJefe { get; set; }
        public string MotivoSep { get; set; }
        public DateTime FechaUltCambio { get; set; }
        public string Observaciones { get; set; }
        public int? EsEmpleoActual { get; set; }

        public virtual RhBtTrab ClaTrabNavigation { get; set; }
    }
}