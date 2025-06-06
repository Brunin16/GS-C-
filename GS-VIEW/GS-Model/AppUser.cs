using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Model
{
    namespace Model
    {
        public class AppUser
        {
            public long Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

            public long? PersonId { get; set; }
            public Person? Person { get; set; }
        }
    }

}
