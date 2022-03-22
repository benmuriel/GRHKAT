using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    [Table("beneficiaire")]
    public class beneficiaire
    {
        public long id { get; set; }
        public string nom_complet { get; set; }
        public DateTime? date_naissance { get; set; }
        public string genre { get; set; }
        public string matricule { get; set; }
        public string grade_carriere { get; set; }
        public string grade_fonction { get; set; }
        public string service { get; set; }
        public string fonction { get; set; }
        public long? service_id { get; set; }
        public int? fonction_id { get; set; }
        public virtual ICollection<charge_sociale> Charge_Sociales { get; set; }

        [JsonIgnore]
        public virtual ICollection<eligibilite_prise_en_charge> eligibilite_prise_en_charges { get; set; }
        [JsonIgnore]
        public  ICollection<engagement> engagements { get; set; }


        public override string ToString()
        {
            return this.nom_complet;
        }
    }
}
