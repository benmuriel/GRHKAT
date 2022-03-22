using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class v_situation_agent_traitement
    {
        public long situation_agent_id { get; set; }
        public int type_traitement_id { get; set; }
        public long agent_id { get; set; }
        public bool nature { get; set; }
        public string type_position { get; set; }
        public int occurence { get; set; }
        public string type_traitement { get; set; }

        public bool allow_charge_sociale { get; set; }

        [NotMapped]
        public string occurence_string
        {
            get
            {
                return this.occurence == 0 ? "Indéterminée" : this.occurence + " fois";
            }
        }
        [NotMapped]
        public string nature_string
        {
            get
            {
                return this.nature == false ? "Retenue" : "Gain";
            }
        }
        [NotMapped]
        public string include_charge_sociale_string
        {
            get
            {
                return this.allow_charge_sociale == true ? "Inclus les charges sociales" : "Individuelle";
            }
        }

        [NotMapped]
        public string occurence_application_string
        {
            get
            {
                return "0";
            }
        }
    }
}
