﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class NetGrupoTiendum
    {
        public NetGrupoTiendum()
        {
            NetTienda = new HashSet<NetTiendum>();
        }

        public int IdGrupoTienda { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<NetTiendum> NetTienda { get; set; }
    }
}