﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class IeTimeIntelligence
    {
        public int IdInforme { get; set; }
        public int IdControlInforme { get; set; }
        public int IdMetrica { get; set; }
        public int IdDimension { get; set; }
        public int IdJerarquia { get; set; }
        public int IdAtributo { get; set; }
        public int IdHecho { get; set; }
        public bool HastaFecha { get; set; }

        public virtual IeAtributo Id { get; set; }
        public virtual IeControlesInforme Id1 { get; set; }
        public virtual IeMetrica IdNavigation { get; set; }
    }
}