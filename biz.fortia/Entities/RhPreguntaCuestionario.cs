﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhPreguntaCuestionario
    {
        public int ClaEmpresa { get; set; }
        public int FolCuestionario { get; set; }
        public int FolPregunta { get; set; }
        public string TextoPregunta { get; set; }
        public int? TipoPregunta { get; set; }
        public int? Orden { get; set; }
        public int FolMedidaCuestionario { get; set; }
        public DateTime? FechaUltCambio { get; set; }
    }
}