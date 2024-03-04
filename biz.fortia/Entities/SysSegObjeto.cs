﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class SysSegObjeto
    {
        public SysSegObjeto()
        {
            SysSegGpoUsuarioObjetos = new HashSet<SysSegGpoUsuarioObjeto>();
        }

        public int ClaObjeto { get; set; }
        public int ClaSistema { get; set; }
        public string ClaveObjeto { get; set; }
        public int ClaModulo { get; set; }
        public string NomObjetoFis { get; set; }
        public string NomObjetoLog { get; set; }
        public string TopicoHelp { get; set; }
        public int? LlevaBit { get; set; }
        public string NomReporte { get; set; }
        public int? Orden { get; set; }
        public string DescBarraStatus { get; set; }
        public byte? EsCatalogoRhn { get; set; }
        public int? Grupo { get; set; }
        public int? OrdenGpo { get; set; }
        public int? EsPantallaIni { get; set; }
        public int? DisponiblePantIni { get; set; }
        public string Agrupador { get; set; }
        public int? AgrupOrden { get; set; }
        public int? AgrupSubOrden { get; set; }
        public int? EsRptEsp { get; set; }
        public string Url { get; set; }
        public byte[] ImagenOpcion { get; set; }
        public int? Modalidad { get; set; }
        public string NomObjetoLogAlias { get; set; }

        public virtual ICollection<SysSegGpoUsuarioObjeto> SysSegGpoUsuarioObjetos { get; set; }
    }
}