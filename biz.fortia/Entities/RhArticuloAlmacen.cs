﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhArticuloAlmacen
    {
        public int ClaAlmacen { get; set; }
        public int ClaArticulo { get; set; }
        public double? Maximo { get; set; }
        public double? Minimo { get; set; }
        public double? PuntoReorden { get; set; }
        public string Localizacion { get; set; }
        public double? Existencia { get; set; }
        public double? TotEntrada { get; set; }
        public double? TotEntradaCant { get; set; }
        public double? TotSalida { get; set; }
        public double? TotSalidaCant { get; set; }
        public double? CostoPromedio { get; set; }
        public DateTime? FechaUltCambio { get; set; }

        public virtual RhAlmacen ClaAlmacenNavigation { get; set; }
        public virtual RhArticulo ClaArticuloNavigation { get; set; }
    }
}