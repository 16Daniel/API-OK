﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Hreservasasunto
    {
        public int Idhotel { get; set; }
        public string Serie { get; set; }
        public int Idreserva { get; set; }
        public string Serieasunto { get; set; }
        public int Numeroasunto { get; set; }
        public int? Idlinea { get; set; }
        public int? Idperiodo { get; set; }
        public int? Idlin { get; set; }
        public int? Idocupante { get; set; }

        public virtual Asunto Asunto { get; set; }
        public virtual Hreservascab Hreservascab { get; set; }
    }
}