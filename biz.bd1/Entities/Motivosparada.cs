﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Motivosparada
    {
        public Motivosparada()
        {
            Serviciosparada = new HashSet<Serviciosparada>();
        }

        public int Codparada { get; set; }
        public string Descparada { get; set; }

        public virtual ICollection<Serviciosparada> Serviciosparada { get; set; }
    }
}