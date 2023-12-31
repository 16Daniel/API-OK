﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhEvaluadoresTrab
    {
        public int FolAuto { get; set; }
        public int ClaEvaluacion { get; set; }
        public int ClaEmpresa { get; set; }
        public DateTime Fecha { get; set; }
        public int ClaTrab { get; set; }
        public int FolEvaluador { get; set; }
        public string NomEvaluador { get; set; }
        public int? TipoEvaluador { get; set; }
        public string Fortalezas { get; set; }
        public string Compromisos { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FechaReferencia { get; set; }
        public int ClaFase { get; set; }
        public string Debilidades { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public byte? PorPropuesta { get; set; }
        public int? Estatus { get; set; }
        public int? EstatusSugerido { get; set; }
        public int? EsSugerido { get; set; }

        public virtual RhCalendarioEvaluacionTrab RhCalendarioEvaluacionTrab { get; set; }
    }
}