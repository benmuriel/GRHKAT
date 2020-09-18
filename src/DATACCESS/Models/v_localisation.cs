using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class v_localisation
    {
        [Key]
        public long id { get; set; }

        public string designation { get; set; }

        public long parent_id { get; set; }


        public int children { get; set; }

        public override string ToString()
        {
            return designation;
        }
    }
}
