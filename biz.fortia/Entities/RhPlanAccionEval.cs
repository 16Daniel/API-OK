﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhPlanAccionEval
    {
        public int ClaEvaluacion { get; set; }
        public int ClaEmpresa { get; set; }
        public int FolAuto { get; set; }
        public int FolPlanAccion { get; set; }
        public int? ClaDepto { get; set; }
        public string NombrePlan { get; set; }
        public string Observaciones { get; set; }
        public byte? Estatus { get; set; }
        public int? TipoPlan { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int ClaFase { get; set; }
    }
}