using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public  class v_situation_agent_required
    {
        public long agent_id { get; set; }
        public long type_position_id { get; set; }
        public string type_position { get; set; }
        public string description { get; set; }
        public DateTime? started_at { get; set; }
    }
}
