﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Comunicacionlog
    {
        public Comunicacionlog()
        {
            Lineascomunicacionlogs = new HashSet<Lineascomunicacionlog>();
            Logzsafacturars = new HashSet<Logzsafacturar>();
        }

        public int Idfront { get; set; }
        public string Tipo { get; set; }
        public DateTime Fechahoraini { get; set; }
        public string Cms { get; set; }
        public DateTime? Fechahoracms { get; set; }
        public int? Estado { get; set; }
        public string Comprimidodesc { get; set; }
        public string Contenido { get; set; }
        public string Realizado { get; set; }
        public string Automatico { get; set; }
        public string Enviado { get; set; }

        public virtual ICollection<Lineascomunicacionlog> Lineascomunicacionlogs { get; set; }
        public virtual ICollection<Logzsafacturar> Logzsafacturars { get; set; }
    }
}