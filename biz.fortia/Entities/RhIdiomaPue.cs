﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhIdiomaPue
    {
        public int ClaPuesto { get; set; }
        public int ClaEmpresa { get; set; }
        public int ClaIdioma { get; set; }
        public string NivelDom { get; set; }
        public int? DominioEscrito { get; set; }
        public int? DominioHablado { get; set; }
        public int? DominioComprension { get; set; }
        public int? DominioLectura { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int? ClaNivelDom { get; set; }
    }
}