﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhUbicacion
    {
        public RhUbicacion()
        {
            RhUniNegUbs = new HashSet<RhUniNegUb>();
        }

        public int ClaUbicacion { get; set; }
        public int ClaEmpresa { get; set; }
        public string NomUbicacion { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Cp { get; set; }
        public string Telefono { get; set; }
        public string Basedatos { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string ZonaSal { get; set; }
        public string RegEdo { get; set; }
        public double? PorcNom { get; set; }
        public string CtaCuadre { get; set; }
        public int? ClaRepresentante { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public string SegCuenta { get; set; }
        public int? ClaRegImss { get; set; }
        public string Delegacion { get; set; }
        public double? PorcNomAdi { get; set; }
        public byte? UsarTabla { get; set; }
        public int? ClaTabImpEdo { get; set; }
        public string TextoAvisoBaja { get; set; }
        public string CuentasAvisoBaja { get; set; }
        public byte? EnviarAvisoBaja { get; set; }
        public byte? BajaActiveDirectory { get; set; }
        public string AdminActiveDirectory { get; set; }
        public string PwdActiveDirectory { get; set; }
        public string NomUbicacionIng { get; set; }
        public int? IdCorpDato { get; set; }
        public int? IdCorporativo { get; set; }
        public string RfcRep { get; set; }
        public string CurpRep { get; set; }
        public string NomRep { get; set; }
        public int? ClaClase { get; set; }
        public string NumRegAmbiental { get; set; }
        public double? LnGrados { get; set; }
        public double? LnMinutos { get; set; }
        public double? LnSegundos { get; set; }
        public double? LoGrados { get; set; }
        public double? LoMinutos { get; set; }
        public double? LoSegundos { get; set; }
        public double? Altitud { get; set; }
        public string NumEstServ { get; set; }
        public int? ClaZona { get; set; }
        public string CorreoRepresentante { get; set; }
        public string ExtTelRepresentante { get; set; }
        public string TelefonoRepresentante { get; set; }
        public string DireccionAuxiliar { get; set; }
        public int? ClaEstado { get; set; }
        public string CpStr { get; set; }
        public string Alfanum1 { get; set; }
        public string Alfanum2 { get; set; }
        public string Alfanum3 { get; set; }

        public virtual ICollection<RhUniNegUb> RhUniNegUbs { get; set; }
    }
}