﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class NetMotivosDescuentoTiendum
    {
        public int IdTienda { get; set; }
        public int IdMotivoDescuento { get; set; }

        public virtual Motivosdto IdMotivoDescuentoNavigation { get; set; }
        public virtual NetTiendum IdTiendaNavigation { get; set; }
    }
}