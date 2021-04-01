using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation
{
    class Route
    {
        public string Number { get; set; }
        public string departureTime { get; set; }
        public string departurePlace { get; set; }
        public string destinationPlace { get; set; }
        public string busNumbers { get; set; }

        public Route(string Number, string departureTime, string departurePlace, string destinationPlace, string busNumbers)
        {
            this.Number = Number;
            this.departureTime = departureTime;
            this.departurePlace = departurePlace;
            this.destinationPlace = destinationPlace;
            this.busNumbers = busNumbers;
        }
    }
}
