﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Pedventamodif
    {
        public string Numserie { get; set; }
        public int Numpedido { get; set; }
        public string N { get; set; }
        public int Numlinea { get; set; }
        public int Fo { get; set; }
        public string Serie { get; set; }
        public short Nummodif { get; set; }
        public string Descripcion { get; set; }
        public double? Incprecio { get; set; }
        public int? Codmodif { get; set; }
        public int? Codarticulo { get; set; }
        public short? Orden { get; set; }
        public short? Nivel { get; set; }
        public bool? Esarticulo { get; set; }
        public double? Dosis { get; set; }
        public double? Unidades { get; set; }
        public bool? Anulado { get; set; }
        public double? Incpreciobase { get; set; }

        public virtual Pedventacab NNavigation { get; set; }
    }
}