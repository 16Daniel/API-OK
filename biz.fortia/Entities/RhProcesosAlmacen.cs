﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhProcesosAlmacen
    {
        public RhProcesosAlmacen()
        {
            RhKardexAlmacens = new HashSet<RhKardexAlmacen>();
        }

        public int ClaProceso { get; set; }
        public string NomProceso { get; set; }
        public int TipoProceso { get; set; }
        public byte? AfectaCostoSino { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public int Visible { get; set; }

        public virtual ICollection<RhKardexAlmacen> RhKardexAlmacens { get; set; }
    }
}