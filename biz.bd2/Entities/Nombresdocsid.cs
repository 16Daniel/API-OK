﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Nombresdocsid
    {
        public Nombresdocsid()
        {
            Documsdisenyids = new HashSet<Documsdisenyid>();
            Icgdocumentos = new HashSet<Icgdocumento>();
            NombresdocsidGruposalmacens = new HashSet<NombresdocsidGruposalmacen>();
        }

        public short Codgrupo { get; set; }
        public int Coddocumento { get; set; }
        public string Descripcion { get; set; }
        public string Impresionrapida { get; set; }
        public int? Codtitulo { get; set; }
        public int? Grupo { get; set; }
        public int? Tipodiseny { get; set; }
        public byte[] Version { get; set; }

        public virtual ICollection<Documsdisenyid> Documsdisenyids { get; set; }
        public virtual ICollection<Icgdocumento> Icgdocumentos { get; set; }
        public virtual ICollection<NombresdocsidGruposalmacen> NombresdocsidGruposalmacens { get; set; }
    }
}