using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class motif_type_position
    {
        public int id { get; set; }
        public string designation { get; set; }
        public long  type_position_id { get; set; }
        public short  duree_max { get; set; }    
        public short  duree_min { get; set; }
        public bool reduce_planning_days { get; set; } 
    }
}
