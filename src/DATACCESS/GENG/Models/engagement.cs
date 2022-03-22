using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    [Table("engagement")]
    public class engagement
    { 
        public long beneficiaire_id { get; set; }
        public long situation_agent_id { get; set; }
        public int type_prise_en_charge_id { get; set; }
        public long projet_id { get; set; } 
        public int? taux_change_id { get; set; }
        public int? bareme_id { get; set; }
        public int? instance_id { get; set; }
        public int? institution_medicale_id { get; set; }
        public long? charge_sociale_id { get; set; } 
        public decimal montant { get; set; }
        public DateTime created_at { get; set; }

        public virtual beneficiaire beneficiaire { get; set; }
        public virtual bareme bareme { get; set; }
        public charge_sociale charge_Sociale { get; set; }

        public projet_engagement projet_engagement { get; set; }
        public virtual type_prise_en_charge type_prise_en_charge { get; set; }

        public instance_traitement Instance_execution { get; set; }
    }
}
