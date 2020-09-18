using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class fonction
    {
        [Key]
        public int id { get; set; }
        public string designation { get; set; }

        public bool is_politic { get; set; }

        public string grade_id { get; set; }
        public override string ToString()
        {
            return designation;
        }
    }
}
