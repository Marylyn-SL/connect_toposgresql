using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_toposgres
{
    internal class Citizens
    {
        public static string Query = "SELECT * FROM Citizens";
        public double citiz_id { get; set; }
        public string citiz_name { get; set; }
        public double citiz_nbr { get; set; }
        public double dis_id { get; set; }
    }
}
