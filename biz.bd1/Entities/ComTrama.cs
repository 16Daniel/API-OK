﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class ComTrama
    {
        public ComTrama()
        {
            ComConfigtramas = new HashSet<ComConfigtrama>();
        }

        public int Iddispositivo { get; set; }
        public int Idoperacion { get; set; }
        public int Idtrama { get; set; }
        public int? Longitud { get; set; }
        public string Marcainicio { get; set; }
        public string Marcafin { get; set; }
        public string Activo { get; set; }
        public int? Idtramaresp { get; set; }

        public virtual ComDispositivo IddispositivoNavigation { get; set; }
        public virtual ComOperacione IdoperacionNavigation { get; set; }
        public virtual ICollection<ComConfigtrama> ComConfigtramas { get; set; }
    }
}