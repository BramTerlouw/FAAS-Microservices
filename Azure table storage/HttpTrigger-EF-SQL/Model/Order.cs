﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpTrigger_EF_SQL.Model
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int OrderType { get; set; }
    }
}
