﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhEncuestaPlanAvance
    {
        public int FolAuto { get; set; }
        public int ClaEmpresa { get; set; }
        public int FolCuestionario { get; set; }
        public int FolEncuesta { get; set; }
        public int FolPlan { get; set; }
        public int FolPlanDet { get; set; }
        public double Porcentaje { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaAvance { get; set; }
        public int? IdSesion { get; set; }
        public DateTime? FechaUltCambio { get; set; }
    }
}