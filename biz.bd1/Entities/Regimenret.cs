﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Regimenret
    {
        public Regimenret()
        {
            Articulosretencionesbases = new HashSet<Articulosretencionesbase>();
            Articulosretencionesivas = new HashSet<Articulosretencionesiva>();
        }

        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Claveretarticulo { get; set; }

        public virtual ICollection<Articulosretencionesbase> Articulosretencionesbases { get; set; }
        public virtual ICollection<Articulosretencionesiva> Articulosretencionesivas { get; set; }
    }
}