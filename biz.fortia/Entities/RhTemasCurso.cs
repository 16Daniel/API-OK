﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhTemasCurso
    {
        public RhTemasCurso()
        {
            RhTemasCursoDets = new HashSet<RhTemasCursoDet>();
        }

        public int FolAuto { get; set; }
        public int ClaCurso { get; set; }
        public string NomTemario { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }

        public virtual ICollection<RhTemasCursoDet> RhTemasCursoDets { get; set; }
    }
}