﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class RhExHorarioCaptura
    {
        public int ClaUbicacion { get; set; }
        public int ClaEmpresa { get; set; }
        public int ClaUsuario { get; set; }
        public int ClaProceso { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime? HoraInicial { get; set; }
        public DateTime? HoraFinal { get; set; }
        public byte AplicaTodoDia { get; set; }
        public int ClaUsuarioAutoriza { get; set; }
    }
}