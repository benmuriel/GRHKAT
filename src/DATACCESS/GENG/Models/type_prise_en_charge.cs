using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
   public class type_prise_en_charge
    {
        public int id { get; set; }

        [Display(Name = "Type de prises en charge")]
        public string designation { get; set; }

        public bool allow_charge_sociale { get; set; }
        public bool allow_inst_medicale { get; set; }
        public bool allow_remboursement { get; set; }
        public bool allow_funding { get; set; }

        public int? instance_engagement_id { get; set; }

        public virtual instance_traitement instance_engagement { get; set; }
        public int? instance_execution_id { get; set; }

        public virtual instance_traitement instance_execution { get; set; }
        public int? instance_liquidation_id { get; set; }

        public virtual instance_traitement instance_liquidation { get; set; }
        [JsonIgnore]
        public  ICollection<projet_engagement> projet_Engagements { get; set; }
        [JsonIgnore]
        public virtual ICollection<eligibilite_prise_en_charge> eligibilite_prise_en_charges { get; set; }
        [JsonIgnore]
        public virtual ICollection<engagement> engagements { get; set; }
        [JsonIgnore]
        public virtual ICollection<bareme> baremes { get; set; }
        public override string ToString()
        {
            return this.designation;
        }
    }
}
