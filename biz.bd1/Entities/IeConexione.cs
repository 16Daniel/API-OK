﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class IeConexione
    {
        public IeConexione()
        {
            IeCubos = new HashSet<IeCubo>();
        }

        public int IdConexion { get; set; }
        public string ServidorBd { get; set; }
        public string NombreBd { get; set; }
        public string UsuarioBd { get; set; }
        public string ContrasenyaBd { get; set; }

        public virtual ICollection<IeCubo> IeCubos { get; set; }
    }
}