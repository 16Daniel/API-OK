﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhCalendarioEvaluacion
    {
        public RhCalendarioEvaluacion()
        {
            RhCalendarioEvaluacionTrabs = new HashSet<RhCalendarioEvaluacionTrab>();
        }

        public int ClaEvaluacion { get; set; }
        public int ClaEmpresa { get; set; }
        public int FolAuto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public byte? Estatus { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int ClaFase { get; set; }
        public int? ClaPlazoVencimiento { get; set; }
        public DateTime? FechaLimite { get; set; }
        public int? MensajePostfecha { get; set; }
        public int? MensajeAntfecha { get; set; }
        public byte? RevisaPerfil { get; set; }
        public int? ClaPlazo { get; set; }
        public byte? ObjetivosAcciones { get; set; }
        public byte? ObjetivosDnc { get; set; }
        public int? ClaMatriz { get; set; }
        public int? TipoEval { get; set; }
        public int ClaPeriodoObjetivo { get; set; }
        public int? FolDesempenioAnt { get; set; }
        public int? FolDesarrolloAnt { get; set; }
        public string NomEval { get; set; }
        public DateTime? FechaIniCorto { get; set; }
        public DateTime? FechaFinCorto { get; set; }
        public DateTime? FechaIniMediano { get; set; }
        public DateTime? FechaFinMediano { get; set; }
        public DateTime? FechaIniLargo { get; set; }
        public DateTime? FechaFinLargo { get; set; }
        public int? ClaSistema { get; set; }
        public int? FolPeriodoObj { get; set; }
        public int PermiteCopiarEval { get; set; }
        public int? PlantillaFuncJera { get; set; }
        public int? NumSolCteInt { get; set; }
        public int? NumSolLaterales { get; set; }
        public int? ActualizoComp { get; set; }
        public int? FolObjGpoEstruct { get; set; }
        public int? FolObjGpoEstructEstatus { get; set; }
        public int? PlanDesarrollo { get; set; }
        public int? CalifObjGpoEstatus { get; set; }

        public virtual ICollection<RhCalendarioEvaluacionTrab> RhCalendarioEvaluacionTrabs { get; set; }
    }
}