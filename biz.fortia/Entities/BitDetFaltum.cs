﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class BitDetFaltum
    {
        public int ClaUsuario { get; set; }
        public int ClaSistema { get; set; }
        public DateTime FechaOper { get; set; }
        public int FolBitacora { get; set; }
        public int ClaTrab { get; set; }
        public int ClaEmpresa { get; set; }
        public int FolAuto { get; set; }
        public int ClaFalta { get; set; }
        public DateTime? FechaInicio { get; set; }
        public double? HrsFalta { get; set; }
        public double? DiasFalta { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FolioInc { get; set; }
        public int? Inicial { get; set; }
        public string TipoFalta { get; set; }
        public int? TipoRiesgo { get; set; }
        public int? ClaRegImss { get; set; }
        public string FolioIncAnt { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaReal { get; set; }
        public byte? TipoOper { get; set; }
        public string Localizacion { get; set; }
    }
}