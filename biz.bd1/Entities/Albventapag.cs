﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Albventapag
    {
        public string Numserie { get; set; }
        public int Numalbaran { get; set; }
        public string N { get; set; }
        public int Numlinea { get; set; }
        public int Fo { get; set; }
        public string Serie { get; set; }
        public int? Codformapago { get; set; }
        public int? Codmoneda { get; set; }
        public double? Importe { get; set; }
        public double? Importe2 { get; set; }
        public double? Entregado { get; set; }
        public double? Cambio { get; set; }
        public double? Cambio2 { get; set; }
        public double? Propina { get; set; }
        public double? Beneficio { get; set; }
        public double? Pendiente { get; set; }
        public DateTime? Fechavencim { get; set; }

        public virtual Albventacab NNavigation { get; set; }
    }
}