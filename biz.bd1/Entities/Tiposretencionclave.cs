﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Tiposretencionclave
    {
        public Tiposretencionclave()
        {
            Tiposretencions = new HashSet<Tiposretencion>();
        }

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Tiposretencion> Tiposretencions { get; set; }
    }
}