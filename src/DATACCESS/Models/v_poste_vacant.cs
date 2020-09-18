using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class v_poste_vacant
    {
        [Key]
        public long id { get; set; }
        public string grade_fonction { get; set; }
        public int fonction_id { get; set; }

        public string fonction { get; set; }
        public bool is_politic { get; set; }
        public short structure_id { get; set; }
        public string structure { get; set; }
        public string designation { get; set; }

        [NotMapped]
        public string fullname { get { return (this.fonction + " @ " + this.structure).Trim(); } }

        public override string ToString()
        {
            return fullname;
        }
    }
}
