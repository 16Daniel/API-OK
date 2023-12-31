﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhCompromiso
    {
        public RhCompromiso()
        {
            RhDncEvalCompromisos = new HashSet<RhDncEvalCompromiso>();
        }

        public int FolCompromiso { get; set; }
        public int ClaEvaluacion { get; set; }
        public int ClaEmpresa { get; set; }
        public int FolAuto { get; set; }
        public int ClaTrab { get; set; }
        public int Tipo { get; set; }
        public int FolPeriodoObjetivos { get; set; }
        public string Descripcion { get; set; }
        public int Estatus { get; set; }
        public int Origen { get; set; }
        public int? FolioCompetencia { get; set; }
        public int? ClaSeccion { get; set; }
        public int? ClaPregunta { get; set; }
        public string Efectividad { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaUltCambio { get; set; }
        public int? ClaCurso { get; set; }
        public string CursoSugerido { get; set; }
        public int? ClaPuesto { get; set; }
        public int? Plazo { get; set; }
        public DateTime? FechaIniReal { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public double? PctAvance { get; set; }
        public string Justificacion { get; set; }
        public int? ClaEmpresaPue { get; set; }
        public int? ClaCompetencia { get; set; }
        public int? ClaEvaluador { get; set; }
        public int? FolReferencia { get; set; }
        public string ComentariosEvaluador { get; set; }
        public string ComentariosEvaluado { get; set; }
        public int? FolObjetivo { get; set; }
        public DateTime? FechaFinOriginal { get; set; }
        public DateTime? FechaIniOriginal { get; set; }
        public int? FolAccionNivel { get; set; }

        public virtual ICollection<RhDncEvalCompromiso> RhDncEvalCompromisos { get; set; }
    }
}