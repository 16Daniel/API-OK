﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhAhorro
    {
        public RhAhorro()
        {
            RhEmpresaAhorros = new HashSet<RhEmpresaAhorro>();
        }

        public int ClaAhorro { get; set; }
        public int Clase { get; set; }
        public string NomAhorro { get; set; }
        public int MesInicial { get; set; }
        public decimal PctEmp { get; set; }
        public decimal PctTrab { get; set; }
        public byte Intereses { get; set; }
        public byte DistIntereses { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int? TipoDistribuyeRendimiento { get; set; }
        public int? TipoRendimiento { get; set; }

        public virtual ICollection<RhEmpresaAhorro> RhEmpresaAhorros { get; set; }
    }
}