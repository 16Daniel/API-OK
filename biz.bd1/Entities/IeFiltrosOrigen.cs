﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class IeFiltrosOrigen
    {
        public IeFiltrosOrigen()
        {
            IeValoresFiltrosOrigens = new HashSet<IeValoresFiltrosOrigen>();
        }

        public int IdCubo { get; set; }
        public int IdFiltroOrigen { get; set; }
        public int IdCampoRelacional { get; set; }
        public int Comparador { get; set; }

        public virtual IeCamposRelacionale IdCampoRelacionalNavigation { get; set; }
        public virtual IeCubo IdCuboNavigation { get; set; }
        public virtual ICollection<IeValoresFiltrosOrigen> IeValoresFiltrosOrigens { get; set; }
    }
}