﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Albventalinpromocione
    {
        public string Numserie { get; set; }
        public int Numalbaran { get; set; }
        public string N { get; set; }
        public int Numlin { get; set; }
        public int Idpromocion { get; set; }
        public double? Importepromocion { get; set; }
        public double? Importepromocioniva { get; set; }

        public virtual Albventalin NNavigation { get; set; }
    }
}