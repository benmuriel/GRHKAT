using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    public class eligibilite_prise_en_charge
    {
        public long beneficiaire_id { get; set; }
        public long situation_agent_id { get; set; }
        public int type_prise_en_charge_id { get; set; }
         public string type_position { get; set; }
        public bool nature { get; set; }
        public int occurence { get; set; }
        public int done_occurence { get; set; }
        public int taux_application_bareme { get; set; }

        public DateTime validated_at { get; set; }
        public DateTime? ended_at { get; set; }
        public bool include_charge_sociale { get; set; }

        public virtual beneficiaire beneficiaire { get; set; }

        [JsonIgnore]
        public virtual type_prise_en_charge type_prise_en_charge { get; set; }

        [NotMapped]
        public string validated_at_string
        {
            get
            {
                return this.validated_at.ToString("dd/MM/yyyy");
            }
        }
        
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
                return this.nature == false ? "(Retenue)" : "(Gain)";
            }
        }
        [NotMapped]
        public string include_charge_sociale_string
        {
            get
            {
                return this.include_charge_sociale == true ? "(Inclus les charges sociales)" : "";
            }
        }
    }
}
