﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DTOs
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public int BilingId { get; set; }
        public int VehicleId { get; set; }
        public string UsedTime { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }

    }
}
