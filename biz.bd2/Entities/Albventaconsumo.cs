﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Albventaconsumo
    {
        public string Numserie { get; set; }
        public int Numalbaran { get; set; }
        public string N { get; set; }
        public int Numlinea { get; set; }
        public int Fo { get; set; }
        public string Serie { get; set; }
        public int Codarticulo { get; set; }
        public double? Consumo { get; set; }
        public string Codalmacen { get; set; }

        public virtual Albventacab NNavigation { get; set; }
    }
}