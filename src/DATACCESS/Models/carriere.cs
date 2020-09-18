using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public  class carriere : situation_agent
    {
        public carriere()
        {
            type_carriere = "normal";
        }
        public string matricule { get; set; }
        public string type_carriere { get; set; }
        public long? coprs_metier_id { get; set; }

        public override string ToString()
        {
            return this.matricule;
        }
    }
}
