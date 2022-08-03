using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public class DeviceJson
    {
        public int id { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string device_health { get; set; }
        public DateTime last_used { get; set; }
        public decimal price { get; set; }
        public string color { get; set; }
    }
}
