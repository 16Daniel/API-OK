﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Icgnombresinforme
    {
        public Icgnombresinforme()
        {
            Icgconsultassqls = new HashSet<Icgconsultassql>();
            Icginformes = new HashSet<Icginforme>();
            Informestycs = new HashSet<Informestyc>();
            Temporalestycs = new HashSet<Temporalestyc>();
        }

        public int Codigogrupo { get; set; }
        public int Codigoinforme { get; set; }
        public int? Tienediseny { get; set; }
        public DateTime? Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Visible { get; set; }
        public int? Codtitulo { get; set; }

        public virtual ICollection<Icgconsultassql> Icgconsultassqls { get; set; }
        public virtual ICollection<Icginforme> Icginformes { get; set; }
        public virtual ICollection<Informestyc> Informestycs { get; set; }
        public virtual ICollection<Temporalestyc> Temporalestycs { get; set; }
    }
}