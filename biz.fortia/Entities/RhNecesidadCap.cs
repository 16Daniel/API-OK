﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhNecesidadCap
    {
        public int ClaNecesidad { get; set; }
        public int ClaEmpresa { get; set; }
        public string NomNecesidad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Observacion { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int? ClaTipoEscala { get; set; }
        public int? ClaEvaluacion { get; set; }
        public int? FolEvaluacion { get; set; }
        public byte? Anunciado { get; set; }
        public byte? Activo { get; set; }
        public byte Programado { get; set; }
        public int? ClaPlanAnual { get; set; }
        public string ClaveOrigen { get; set; }
    }
}