﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RptReporte
    {
        public RptReporte()
        {
            RptVistaReportes = new HashSet<RptVistaReporte>();
        }

        public string IdReporte { get; set; }
        public string NomReporte { get; set; }
        public string Descripcion { get; set; }
        public string NomProceso { get; set; }
        public string NomSubproceso { get; set; }
        public byte[] Reporte { get; set; }
        public string ClaveObjeto { get; set; }
        public string NomModulo { get; set; }

        public virtual ICollection<RptVistaReporte> RptVistaReportes { get; set; }
    }
}