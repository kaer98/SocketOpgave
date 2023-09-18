using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketOpgave.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float MinPrice { get; set; }
        public float SalePrice { get; set; }
        public TimeSpan AuctionLength { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get{ return StartTime.Add(AuctionLength); } }



    }
}
