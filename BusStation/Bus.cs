using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation
{
    class Bus
    {
        public string busNumber { get; set; }
        public string routeNumber { get; set; }
        public int capacity { get; set; }

        public Bus(string busNumber, string routeNumber, int capacity)
        {
            this.busNumber = busNumber;
            this.routeNumber = routeNumber;
            this.capacity = capacity;
        }
    }
}
