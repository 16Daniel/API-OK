﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class RemSala
    {
        public RemSala()
        {
            RemConfigsalas = new HashSet<RemConfigsala>();
        }

        public int Idfront { get; set; }
        public short Sala { get; set; }
        public string Nombre { get; set; }
        public bool? Desactmesas { get; set; }

        public virtual RemFront IdfrontNavigation { get; set; }
        public virtual ICollection<RemConfigsala> RemConfigsalas { get; set; }
    }
}