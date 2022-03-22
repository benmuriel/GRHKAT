using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    [Table("structure")]
   public class structure
    {
        public short id { get; set; }
        public string designation { get; set; }
        public long? type_structure_id { get; set; }
        public short lvl { get; set; }
        public short? parent_id { get; set; }

        public virtual ICollection<planning_project> planning_projects { get; set; }        
    }
}
