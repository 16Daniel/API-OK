﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Dispositivoslin
    {
        public int Idterminal { get; set; }
        public string Tipodispositivo { get; set; }
        public string Nombre { get; set; }
        public int Posicion { get; set; }
        public string Secuencia { get; set; }

        public virtual Dispositivo Dispositivo { get; set; }
    }
}