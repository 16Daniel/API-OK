﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class NombresdocsidGruposalmacen
    {
        public short Codgrupo { get; set; }
        public int Coddocumento { get; set; }
        public int Codgrupoalmacen { get; set; }

        public virtual Nombresdocsid Cod { get; set; }
    }
}