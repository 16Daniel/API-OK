﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Hcuposservicio
    {
        public int Idhotel { get; set; }
        public int Idcupo { get; set; }
        public int Codactividad { get; set; }
        public int Codservicio { get; set; }
        public int? Posicion { get; set; }

        public virtual Tipoasunto CodactividadNavigation { get; set; }
        public virtual Serviciosglobale CodservicioNavigation { get; set; }
        public virtual Hcupo IdcupoNavigation { get; set; }
        public virtual Hotele IdhotelNavigation { get; set; }
    }
}