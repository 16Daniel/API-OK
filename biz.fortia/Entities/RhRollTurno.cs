﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhRollTurno
    {
        public int ClaEmpresa { get; set; }
        public int ClaRoll { get; set; }
        public string NomRoll { get; set; }
        public DateTime? FechaUltCambio { get; set; }
        public double? HorasProm { get; set; }
        public int? ClaTrabRef1 { get; set; }
        public int? ClaTrabRef2 { get; set; }
        public int? ClaTrabRef3 { get; set; }
        public int? ClaTrabRef4 { get; set; }
        public string Reviso { get; set; }
        public int? ClaPerdedPd { get; set; }
        public int? ClaPerdedFestivo { get; set; }
        public int? ClaPerdedDescanso { get; set; }
        public double? DiasLabPer { get; set; }
        public double? SepDiaPer { get; set; }
        public byte? Ignorarcortes { get; set; }
        public double? HorasSem { get; set; }
        public byte? Nodescalimfest { get; set; }
        public byte? Nodescalimdesc { get; set; }
        public int? Entantdom { get; set; }
        public int? Tiptopentantdom { get; set; }
        public int? Topeentantdom { get; set; }
        public int? Entoturendom { get; set; }
        public int? Tiptopentoturendom { get; set; }
        public int? Topeentoturendom { get; set; }
        public int? Generarprimdom { get; set; }
        public int? Topepagodesc { get; set; }
        public int? Topepagofest { get; set; }
        public int? Generartefest { get; set; }
        public int? Descuentoalim { get; set; }
        public byte? Descalimapartir { get; set; }
        public byte? Descalimcada { get; set; }
        public byte? Gencortefest { get; set; }
        public byte? Genretardofest { get; set; }
        public int? Tiempomaxretardo { get; set; }
        public int? InicioRetardo { get; set; }
        public byte? Tipogeninciord { get; set; }
        public int? Hrsmaxsigsalida { get; set; }
        public int? Hrsmaxpreventrada { get; set; }
        public byte? GenerarDescTrab { get; set; }
        public byte? GenerarFestTrab { get; set; }
        public byte? Genretardodesc { get; set; }
        public int? Tolretardoendiasinturno { get; set; }
        public int? Tolhrsextentendiasinturno { get; set; }
        public int? Tolhrsextsalendiasinturno { get; set; }
    }
}