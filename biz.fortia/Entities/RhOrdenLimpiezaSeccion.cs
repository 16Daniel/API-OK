﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhOrdenLimpiezaSeccion
    {
        public RhOrdenLimpiezaSeccion()
        {
            RhOrdenLimpiezaGuiaDets = new HashSet<RhOrdenLimpiezaGuiaDet>();
            RhOrdenLimpiezaSeccionDets = new HashSet<RhOrdenLimpiezaSeccionDet>();
        }

        public int FolSeccion { get; set; }
        public int ClaEmpresa { get; set; }
        public string NomSeccion { get; set; }
        public DateTime FechaUltCambio { get; set; }

        public virtual ICollection<RhOrdenLimpiezaGuiaDet> RhOrdenLimpiezaGuiaDets { get; set; }
        public virtual ICollection<RhOrdenLimpiezaSeccionDet> RhOrdenLimpiezaSeccionDets { get; set; }
    }
}