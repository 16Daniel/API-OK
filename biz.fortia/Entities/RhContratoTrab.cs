﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhContratoTrab
    {
        public int FolContrato { get; set; }
        public int ClaEmpresa { get; set; }
        public int ClaTrab { get; set; }
        public int ClaContrato { get; set; }
        public int? TipoContratoImss { get; set; }
        public int? DiasContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Observaciones { get; set; }
        public int? FolReferencia { get; set; }
        public int? ClaUsuario { get; set; }
        public int? Origen { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int? Agrupador { get; set; }
    }
}