﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Icgconsultassql
    {
        public int Grupo { get; set; }
        public int Icgconsulta { get; set; }
        public string Tipo { get; set; }
        public string Tipoparam { get; set; }
        public string Nombreparam { get; set; }
        public int Ncampo { get; set; }
        public string Iconsulta { get; set; }
        public string Valor { get; set; }
        public int? Codtitulo { get; set; }

        public virtual Icgnombresinforme Icgnombresinforme { get; set; }
    }
}