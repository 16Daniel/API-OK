﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class HioposScoreboard
    {
        public HioposScoreboard()
        {
            HioposScoreboardInformes = new HashSet<HioposScoreboardInforme>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int? Codusuario { get; set; }

        public virtual ICollection<HioposScoreboardInforme> HioposScoreboardInformes { get; set; }
    }
}