﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class RemCajasfrontsseriessubempresa
    {
        public int Idfront { get; set; }
        public int Cajafront { get; set; }
        public int Subempresa { get; set; }
        public int Tipodoc { get; set; }
        public string Serie { get; set; }

        public virtual RemFront IdfrontNavigation { get; set; }
    }
}