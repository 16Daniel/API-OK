﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class CmrcFotosarticulo
    {
        public int Codarticulo { get; set; }
        public int Posicion { get; set; }
        public int? Orden { get; set; }
        public string Portada { get; set; }
        public int? Codfoto { get; set; }
        public byte[] Version { get; set; }
        public int Idimageps { get; set; }

        public virtual Articulo1 CodarticuloNavigation { get; set; }
    }
}