using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class v_poste
    {
        [Key]
        public long id { get; set; }
        public string grade_fonction { get; set; }

        public Int16 poste_order_number { get; set; }
        public int fonction_id { get; set; }
        public string designation { get; set; }
        public string fonction { get; set; }
        public bool is_politic { get; set; }
        public short structure_id { get; set; }
        public string structure { get; set; } 

        public long? agent_id { get; set; }
        public string occupant { get; set; }
        public string genre { get; set; }
        public string matricule { get; set; } 
        public string grade_carriere { get; set; }
        public string etat_poste { get; set; }
        public override string ToString()
        {
            return fonction;
        }

        public string TypeFonction
        {
            get
            {
                return this.is_politic ? "Fonction politique" : "Fonction administratif";
            }
        }
    }
}
