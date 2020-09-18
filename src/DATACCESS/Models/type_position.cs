using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class type_position
    {
        public long id { get; set; }
        public string categorie { get; set; }
        public string designation { get; set; }
        public int duree_max_position { get; set; } 
        public bool has_poste { get; set; }
        public bool has_institution { get; set; }
        public bool ends_emploi { get; set; }
        public categorie_type_position categorie_Type_Position { get; set; }

        public override string ToString()
        {
            return this.designation;
        }
    }
}
