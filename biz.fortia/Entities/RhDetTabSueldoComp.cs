﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhDetTabSueldoComp
    {
        public int FolAuto { get; set; }
        public int ClaSueComp { get; set; }
        public double SueldoDesde { get; set; }
        public double SueldoHasta { get; set; }
        public double PctSueldo { get; set; }
        public double PctSueadi { get; set; }
        public DateTime? FechaUltCambio { get; set; }

        public virtual RhTabSueldoComp ClaSueCompNavigation { get; set; }
    }
}