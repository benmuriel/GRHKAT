using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public  class v_structure
    {
        [Key]
        public short id { get; set; }

        public long? type_structure_id { get; set; }
        public string type_structure { get; set; }
        public bool is_affectation { get; set; }
        public string structure { get; set; }
        public string designation { get; set; }

        public short? parent_id { get; set; }
        public long? poste_id { get; set; }
      
        public short lvl { get; set; }

        public override string ToString()
        {
            return structure;
        }

        public string fullName
        {
            get
            {
                return this.structure.Trim();
            }
        }
    }
}
