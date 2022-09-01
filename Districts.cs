using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_toposgres
{
    internal class Districts
    {
        public static string Query = "SELECT * FROM Districts";
        public double dis_id { get; set; }
        public string dis_name { get; set; }
    }
}
