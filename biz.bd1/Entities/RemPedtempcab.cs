﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class RemPedtempcab
    {
        public RemPedtempcab()
        {
            RemPedtemplins = new HashSet<RemPedtemplin>();
        }

        public int Idpedido { get; set; }
        public string Supedido { get; set; }
        public int? Codcliente { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? Fechaentrega { get; set; }
        public string Enviopor { get; set; }
        public double? Totalneto { get; set; }

        public virtual ICollection<RemPedtemplin> RemPedtemplins { get; set; }
    }
}