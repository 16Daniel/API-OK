﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class IeCamposRelacionale
    {
        public IeCamposRelacionale()
        {
            IeCamposOrigenesRelacionales = new HashSet<IeCamposOrigenesRelacionale>();
            IeFiltrosOrigens = new HashSet<IeFiltrosOrigen>();
        }

        public int IdCampoRelacional { get; set; }
        public string TablaRelacional { get; set; }
        public string CampoRelacional { get; set; }
        public int TipoRelacional { get; set; }
        public int Tamanyo { get; set; }

        public virtual ICollection<IeCamposOrigenesRelacionale> IeCamposOrigenesRelacionales { get; set; }
        public virtual ICollection<IeFiltrosOrigen> IeFiltrosOrigens { get; set; }
    }
}