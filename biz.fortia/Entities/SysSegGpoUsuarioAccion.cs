﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.fortia.Entities
{
    public partial class SysSegGpoUsuarioAccion
    {
        public int ClaObjeto { get; set; }
        public int ClaModulo { get; set; }
        public int ClaGpoUsuario { get; set; }
        public int ClaSistema { get; set; }
        public int ClaAccion { get; set; }
        public short TipoAccion { get; set; }

        public virtual SysSegAccion Cla { get; set; }
        public virtual SysGpoUsuario ClaGpoUsuarioNavigation { get; set; }
    }
}