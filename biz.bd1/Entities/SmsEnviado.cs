﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class SmsEnviado
    {
        public int Idsms { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Mobil { get; set; }
        public int? Codusuario { get; set; }

        public virtual SmsTexto IdsmsNavigation { get; set; }
    }
}