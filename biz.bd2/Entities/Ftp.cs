﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Ftp
    {
        public int Idftp { get; set; }
        public string Servidor { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int? Puerto { get; set; }
        public string Carpetaimport { get; set; }
        public string Carpetaexport { get; set; }
    }
}