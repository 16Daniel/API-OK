﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd1.Entities
{
    public partial class Dispositivo
    {
        public Dispositivo()
        {
            Dispositivoslins = new HashSet<Dispositivoslin>();
        }

        public int Idterminal { get; set; }
        public string Tipodispositivo { get; set; }
        public string Nombre { get; set; }
        public string Opciones { get; set; }
        public string Secuencia1 { get; set; }
        public string Secuencia2 { get; set; }
        public string Formato { get; set; }
        public int Caracs { get; set; }
        public double? Importe { get; set; }
        public int? Longitud1 { get; set; }
        public int? Longitud2 { get; set; }
        public string Caja { get; set; }
        public string Impresoracajon { get; set; }
        public string Directoriorespuestas { get; set; }
        public string Directoriocashdro { get; set; }

        public virtual ICollection<Dispositivoslin> Dispositivoslins { get; set; }
    }
}