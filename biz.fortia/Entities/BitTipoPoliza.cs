﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class BitTipoPoliza
    {
        public int ClaUsuario { get; set; }
        public int ClaSistema { get; set; }
        public DateTime FechaOper { get; set; }
        public int FolBitacora { get; set; }
        public int ClaTipoPoliza { get; set; }
        public int ClaEmpresa { get; set; }
        public string NomTipoPoliza { get; set; }
        public string CtaCuadre { get; set; }
        public int? PorUbicacion { get; set; }
        public int? PorDepto { get; set; }
        public int? PorCentroCosto { get; set; }
        public byte? PorTrabajador { get; set; }
        public byte? UsarCuentaTrab { get; set; }
        public byte? TipoOper { get; set; }
        public string Localizacion { get; set; }
    }
}