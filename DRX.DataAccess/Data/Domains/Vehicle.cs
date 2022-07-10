using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DataAccess.Data.Domains
{
    [Table("VehicleData")]
    public class Vehicle
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public double PricePerMinute { get; set; }
        public string Location { get; set; }
        public string Model { get; set; }
    }
}
