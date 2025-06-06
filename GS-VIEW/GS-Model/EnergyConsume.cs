using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Model
{
    public class EnergyConsume
    {
        public long Id { get; set; }
        public double PricePerHour { get; set; }
        public double FixPrice { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public long EquipamentId { get; set; }
        public Equipament Equipament { get; set; }
    }
}
