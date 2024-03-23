using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Classes
{
    internal class CinemaHall
    {
        public string cinemaHallID {  get; set; }
        public string cinemaHallName { get; set; }

        public CinemaHall(string id, string name)
        {
            cinemaHallID = id;
            cinemaHallName = name;
        }
    }


}
