﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Models
{
    public class RentData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public bool IsActive { get; set; }
        public string LastLocation { get; set; }
        public string RentDate { get; set; }
    }
}
