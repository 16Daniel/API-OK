﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Mappingsfilesmainfield
    {
        public int Idmap { get; set; }
        public int Idfile { get; set; }
        public int Numcampo { get; set; }
        public string Campo { get; set; }

        public virtual Mappingsfile Id { get; set; }
    }
}