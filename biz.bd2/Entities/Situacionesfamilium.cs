﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Situacionesfamilium
    {
        public int Codseccion { get; set; }
        public int Codsituacion { get; set; }
        public byte[] Version { get; set; }

        public virtual Situacione CodsituacionNavigation { get; set; }
    }
}