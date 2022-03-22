using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    [Table("poste")]
   public class poste
    {
        [Key]
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public string grade_id { get; set; }
        public string fonction { get; set; }
        public bool is_politic { get; set; }
        public short structure_id { get; set; } 
       
    }
}
