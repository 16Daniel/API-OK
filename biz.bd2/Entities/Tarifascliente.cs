﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Tarifascliente
    {
        public int Codcliente { get; set; }
        public int Idtarifav { get; set; }
        public string Descripcion { get; set; }
        public int? Posicion { get; set; }
        public double? Dto { get; set; }
        public int? Codproveedor { get; set; }
        public string Codexterno { get; set; }
        public byte[] Version { get; set; }

        public virtual Cliente CodclienteNavigation { get; set; }
        public virtual Tarifasventum IdtarifavNavigation { get; set; }
    }
}