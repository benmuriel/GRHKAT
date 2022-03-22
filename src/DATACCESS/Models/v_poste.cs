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
         
        public string fonction { get; set; }
        public bool is_politic { get; set; }
        public short structure_id { get; set; }
        public string structure { get; set; }

        public long? agent_id { get; set; }
        public string occupant { get; set; }
        public string position_state { get; set; }
        public string motif_occupation { get; set; }
      
        //public string genre { get; set; }
        // public string matricule { get; set; } 
        // public string grade_carriere { get; set; }
        public string etat_poste { get; set; }
        public override string ToString()
        {
            return fonction + " @ " + structure;
        }

        public string TypeFonction
        {
            get
            {
                return this.is_politic ? "Poste politique" : "Poste administratif";
            }
        }
        public string state_string
        {
            get
            {
                return this.position_state == "tovalidate_start" ? "En attente de validation (Debut)"
                    : this.position_state == "tovalidate_end" ? "En attente de validation (Fin)"
                    : this.position_state == "running" ? "En cours"
                    : this.position_state == "comming" ? "En attente de deroulement"
                    : this.position_state == "passed" ? "Terminée" : "";
            }
        }
    }
}
