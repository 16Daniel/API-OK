﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Planta
    {
        public Planta()
        {
            Confighabitaciones = new HashSet<Confighabitacione>();
        }

        public int Idhotel { get; set; }
        public short Planta1 { get; set; }
        public string Nombre { get; set; }
        public byte[] Version { get; set; }
        public string Codalmacen { get; set; }
        public int? Codvendedor { get; set; }

        public virtual ICollection<Confighabitacione> Confighabitaciones { get; set; }
    }
}