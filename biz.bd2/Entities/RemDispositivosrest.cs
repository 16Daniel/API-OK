﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class RemDispositivosrest
    {
        public RemDispositivosrest()
        {
            RemRegistrodispositivosrests = new HashSet<RemRegistrodispositivosrest>();
        }

        public int Idfront { get; set; }
        public string Terminal { get; set; }
        public string Tipodispositivo { get; set; }
        public string Nombre { get; set; }
        public string Opciones { get; set; }
        public string Secuencia { get; set; }
        public string Secuencia2 { get; set; }
        public short? Gruposecuencias { get; set; }
        public short? Numcaja { get; set; }
        public string Impresoracajon { get; set; }

        public virtual RemFront IdfrontNavigation { get; set; }
        public virtual ICollection<RemRegistrodispositivosrest> RemRegistrodispositivosrests { get; set; }
    }
}