﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhDetObjetivosTrab
    {
        public int ClaEmpresa { get; set; }
        public int FolPeriodo { get; set; }
        public int ClaTrab { get; set; }
        public int FolObjetivo { get; set; }
        public int FolAuto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCompromiso { get; set; }
        public DateTime? FechaInicioReal { get; set; }
        public DateTime? FechaFinalReal { get; set; }
        public string Descripcion { get; set; }
        public byte? Estatus { get; set; }
        public double? PctAvance { get; set; }
        public int ClaEscalaEval { get; set; }
        public int? Tipo { get; set; }
    }
}