﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhCorreoEnviado
    {
        public int IdCorreo { get; set; }
        public string Para { get; set; }
        public string ConCopia { get; set; }
        public string Encabezado { get; set; }
        public string Cuerpo { get; set; }
        public DateTime Fecha { get; set; }
        public int FolFirma { get; set; }
        public DateTime FechaEnviado { get; set; }
        public string De { get; set; }
    }
}