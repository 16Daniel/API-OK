﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Mappingsexportacionescab
    {
        public Mappingsexportacionescab()
        {
            Mappingsexportacioneslins = new HashSet<Mappingsexportacioneslin>();
        }

        public int Idexportacion { get; set; }
        public int? Idmap { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual ICollection<Mappingsexportacioneslin> Mappingsexportacioneslins { get; set; }
    }
}