﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class RemModelosimpresorarest
    {
        public int Idfront { get; set; }
        public string Modeloimpresora { get; set; }
        public short? Gruposecuencias { get; set; }

        public virtual RemFront IdfrontNavigation { get; set; }
    }
}