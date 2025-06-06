using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Model
{
    public class Equipament
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double HourUsedPerDay { get; set; }

        public long PersonId { get; set; }
        public Person Person { get; set; }

        public List<EnergyConsume> EnergyConsumes { get; set; } = new();
    }
}
