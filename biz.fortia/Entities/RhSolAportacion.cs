﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhSolAportacion
    {
        public int FolAportacion { get; set; }
        public int? ClaEmpresa { get; set; }
        public int? ClaTrab { get; set; }
        public int? ClaPerded { get; set; }
        public double? Cantidad { get; set; }
        public int? ModoAportacion { get; set; }
        public DateTime? FechaApartirDe { get; set; }
        public DateTime? FechaHasta { get; set; }
        public DateTime? FechaSolicita { get; set; }
        public DateTime? FechaAutoriza { get; set; }
        public int? Estatus { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public string Observaciones { get; set; }
        public int? FolMovNominaRef { get; set; }
    }
}