﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Condicionesgruposocupante
    {
        public int Idgrupo { get; set; }
        public int Grupoor { get; set; }
        public int Grupoand { get; set; }
        public string Incluir { get; set; }
        public int? Tabla { get; set; }
        public string Campo { get; set; }
        public string Operador { get; set; }
        public string Valor { get; set; }

        public virtual Gruposocupante IdgrupoNavigation { get; set; }
    }
}