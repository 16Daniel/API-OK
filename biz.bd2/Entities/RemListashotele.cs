﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class RemListashotele
    {
        public int Idhotel { get; set; }
        public int Tipo { get; set; }
        public int Codigo { get; set; }
        public byte[] Version { get; set; }
        public string Codigostr { get; set; }

        public virtual Hotele IdhotelNavigation { get; set; }
    }
}