using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class v_emploi : situation_agent
    {
        public long poste_id { get; set; }
        public bool is_politic { get; set; }
        public string fonction { get; set; }
        public string grade_fonction { get; set; }
        public short structure_id { get; set; }
        public string structure { get; set; }  

        [NotMapped]
        public string fullname { get { return (this.fonction + " / " + this.structure).Trim(); } }

        public override string ToString()
        {
            return this.fullname;
        }
    }
}
