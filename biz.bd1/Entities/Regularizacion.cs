﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Regularizacion
    {
        public string Codalmacen { get; set; }
        public int Codarticulo { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public double? Unidades { get; set; }
        public double? Stockfinal { get; set; }
        public string Cuadrado { get; set; }

        public virtual Articuloslin Articuloslin { get; set; }
    }
}