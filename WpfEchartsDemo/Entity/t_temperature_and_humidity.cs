using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEchartsDemo.Entity
{
    public class t_temperature_and_humidity
    {
        public string id { get; set; }

        public string asset_id { get; set; }

        public Double temperature { get; set; }

        public Double humidity { get; set; }

        public DateTime last_update_time { get; set; }

        public string warehouse_code { get; set; }
    }
}
