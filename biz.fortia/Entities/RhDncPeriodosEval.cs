﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhDncPeriodosEval
    {
        public RhDncPeriodosEval()
        {
            RhDncEvalCompromisos = new HashSet<RhDncEvalCompromiso>();
        }

        public int FolDncEval { get; set; }
        public int FolEval { get; set; }
        public int ClaEvaluacion { get; set; }
        public int ClaNecesidad { get; set; }
        public int ClaEmpresa { get; set; }
        public DateTime? FechaUltCambio { get; set; }

        public virtual ICollection<RhDncEvalCompromiso> RhDncEvalCompromisos { get; set; }
    }
}