﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhFlujoCondicionWkf
    {
        public int IdFlujoProceso { get; set; }
        public int ClaTabla { get; set; }
        public int ClaCampo { get; set; }
        public string Valor { get; set; }
        public int? Operador { get; set; }
        public int AplicaSobre { get; set; }
        public DateTime FechaUltCambio { get; set; }

        public virtual RhCamposWkf Cla { get; set; }
        public virtual RhFlujoProcesoWkf IdFlujoProcesoNavigation { get; set; }
    }
}