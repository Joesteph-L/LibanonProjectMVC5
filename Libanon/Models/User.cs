﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libanon.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
    }
}