﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhEncuestum
    {
        public int ClaEmpresa { get; set; }
        public int FolCuestionario { get; set; }
        public int FolEncuesta { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int? ClaDepto { get; set; }
        public int? ClaCentroCosto { get; set; }
        public int? ClaPuesto { get; set; }
        public int? ClaUbicacion { get; set; }
        public int? Estatus { get; set; }
        public string NomEncuesta { get; set; }
    }
}