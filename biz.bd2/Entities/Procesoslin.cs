﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Procesoslin
    {
        public int Idproceso { get; set; }
        public string Clave { get; set; }
        public string Subclave { get; set; }
        public string Valor { get; set; }

        public virtual Proceso IdprocesoNavigation { get; set; }
    }
}