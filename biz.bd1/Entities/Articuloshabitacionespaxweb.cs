﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Articuloshabitacionespaxweb
    {
        public int Codarticulo { get; set; }
        public int Paxadult { get; set; }
        public int Paxnen { get; set; }
        public int Paxbebe { get; set; }

        public virtual Articuloshabitacione CodarticuloNavigation { get; set; }
    }
}