﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class TPedidosEntrega
    {
        public TPedidosEntrega()
        {
            TFotosPedidosEntregas = new HashSet<TFotosPedidosEntrega>();
        }

        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public DateTime? FechaProg { get; set; }
        public DateTime? FechaReal { get; set; }
        public string Comentarios { get; set; }
        public int Estatus { get; set; }
        public int IdSucursal { get; set; }

        public virtual TEstatusPedidosEntrega EstatusNavigation { get; set; }
        public virtual ICollection<TFotosPedidosEntrega> TFotosPedidosEntregas { get; set; }
    }
}