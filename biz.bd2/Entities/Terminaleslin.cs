﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Terminaleslin
    {
        public int Idterminal { get; set; }
        public int Tipodoc { get; set; }
        public string Impresora { get; set; }
        public string Disenyimp { get; set; }
        public string Disenymail { get; set; }
        public string Disenyimpn { get; set; }
        public string Dismailn { get; set; }

        public virtual Terminale IdterminalNavigation { get; set; }
    }
}