﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.bd2.Entities
{
    public partial class Dingustazzy
    {
        public int Idhotel { get; set; }
        public bool Descarga { get; set; }
        public bool Subida { get; set; }
        public string Syncrosvcurl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Hotelcode { get; set; }
        public int? Tiporeserva { get; set; }
        public bool Enproduccion { get; set; }
        public bool Mapear { get; set; }
        public bool Maparticulos { get; set; }
        public bool Mapagencias { get; set; }
        public string Fieldarticulos { get; set; }
        public string Fieldagencias { get; set; }

        public virtual Hotele IdhotelNavigation { get; set; }
    }
}