using GS_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Model
{
    public class Person
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int Years { get; set; }
        public string Endress { get; set; }
        public string Country { get; set; }
        public long UserId { get; set; }

        public AppUser User { get; set; }
        public List<Equipament> Equipaments { get; set; } = new();
    }
}
