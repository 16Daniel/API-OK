﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhVersionDescrPuesto
    {
        public int ClaVersion { get; set; }
        public int ClaEmpresa { get; set; }
        public int ClaPuesto { get; set; }
        public string NomVersion { get; set; }
        public string NomCorto { get; set; }
        public string Descripcion { get; set; }
        public int Estatus { get; set; }
        public int Aplicada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAplica { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int? ClaUsuario { get; set; }
        public int? ClaMatriz { get; set; }
    }
}