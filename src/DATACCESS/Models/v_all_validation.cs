using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
  public  class v_all_validation
    {
        public long agent_id { get; set; }
        public string agent { get; set; }
        public string reference { get; set; }
        public DateTime requested_at { get; set; }
        public long objet_id { get; set; }
        public string object_name { get; set; }
        public string details { get; set; }       
    }
}
