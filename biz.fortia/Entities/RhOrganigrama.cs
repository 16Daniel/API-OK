﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhOrganigrama
    {
        public int FolOrganigrama { get; set; }
        public int? ClaEmpresa { get; set; }
        public int? ClaArea { get; set; }
        public int? ClaDepto { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int ClaOrganigrama { get; set; }
        public string NomOrganigrama { get; set; }
        public byte VerNombre { get; set; }
        public byte VerPuesto { get; set; }
        public byte VerCentroCosto { get; set; }
        public byte VerPlaza { get; set; }
        public byte VerUbicacion { get; set; }
        public byte VerDepto { get; set; }
        public byte VerSueldo { get; set; }
        public string Layout { get; set; }
        public string Campos { get; set; }
        public int ClaPlantilla { get; set; }
        public int? TipoOrganigrama { get; set; }
        public int? TipoDependencia { get; set; }
    }
}