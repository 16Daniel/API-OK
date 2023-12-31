﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhCursoCalendarioPlan
    {
        public RhCursoCalendarioPlan()
        {
            RhCursoCalendarioDetPlans = new HashSet<RhCursoCalendarioDetPlan>();
            RhCursoCalendarioTrabPlans = new HashSet<RhCursoCalendarioTrabPlan>();
        }

        public int FolioCurso { get; set; }
        public string NomGrupo { get; set; }
        public int ClaEmpresa { get; set; }
        public int? ClaPeriodoDnc { get; set; }
        public int ClaCurso { get; set; }
        public int TipoCurso { get; set; }
        public DateTime FechaGen { get; set; }
        public int? ClaUsuarioAutoriza { get; set; }
        public DateTime? FechaAutoriza { get; set; }
        public byte Estatus { get; set; }
        public int OrigenCurso { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? ClaInstruc { get; set; }
        public int? ClaSala { get; set; }
        public int? ClaTrabRevisa { get; set; }
        public int? ClaEmpresaRevisa { get; set; }
        public int ClaPlanAnual { get; set; }
        public DateTime? FechaConfirma { get; set; }
        public int? ClaUsuarioConfirma { get; set; }
        public DateTime? FechaCancela { get; set; }
        public int? ClaUsuarioCancela { get; set; }

        public virtual ICollection<RhCursoCalendarioDetPlan> RhCursoCalendarioDetPlans { get; set; }
        public virtual ICollection<RhCursoCalendarioTrabPlan> RhCursoCalendarioTrabPlans { get; set; }
    }
}