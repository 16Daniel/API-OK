﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class SysSegCatObjeto
    {
        public int ClaObjeto { get; set; }
        public string ClaveObjeto { get; set; }
        public string NomObjetoLog { get; set; }
        public string NomObjetoFis { get; set; }
        public byte? EsRptEsp { get; set; }
        public byte[] IconoImagen { get; set; }

        public virtual ICollection<SysSegAgrupador> ClaAgrupadors { get; set; }
    }
}