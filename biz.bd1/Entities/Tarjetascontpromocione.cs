﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Tarjetascontpromocione
    {
        public int Idtarjeta { get; set; }
        public int Idfront { get; set; }
        public int? Puntosacumulados { get; set; }
        public double? Consacumuladas { get; set; }
        public double? Importeacumulado { get; set; }
        public double? Ticketsacumulados { get; set; }

        public virtual Tarjeta IdtarjetaNavigation { get; set; }
    }
}