﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Gruposmappingscab
    {
        public Gruposmappingscab()
        {
            Gruposmappingslins = new HashSet<Gruposmappingslin>();
        }

        public int Idgrupo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Gruposmappingslin> Gruposmappingslins { get; set; }
    }
}