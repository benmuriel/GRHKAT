using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public  class lieu_realisation_type_position
    {
        public int id { get; set; }
        public string designation { get; set; }

        public long type_position_id { get; set; }

    }
}
