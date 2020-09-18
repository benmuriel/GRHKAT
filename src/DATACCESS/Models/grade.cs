using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class grade
    {
        [Key]
        public string id { get; set; }
        public string designation { get; set; }
        public string rang { get; set; }
        public string niveau { get; set; }

        [NotMapped]
        public string describe
        {
            get
            {
                return this.id + " - " + this.designation +" "+this.rang +" " +this.niveau;
            }
        }

        public override string ToString()
        {
            return describe;
        }
    }
}
