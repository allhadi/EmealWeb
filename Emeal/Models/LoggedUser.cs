﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emeal.Models
{
    public class LoggedUser
    {
        public int Id { set; get; }
        public string Email { set; get; }
        public string Name { set; get; }
        public string AccessCode { set; get; }
        public int CompanyId { get; set; }
    }
}