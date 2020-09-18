using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class v_charge_sociale
    {
        [Key]
        public long id { get; set; }
        public long agent_id { get; set; }

        public string nom { get; set; }
        public string post_nom { get; set; }
        public string prenom { get; set; }
        public string nom_complet { get; set; }

        public string genre { get; set; }

        public string lieu_naissance { get; set; }

        public DateTime date_naissance { get; set; }

        public bool etat { get; set; }

        public string occupation { get; set; }
        public string affinite { get; set; }
        public string matricule { get; set; }
        public string service { get; set; }

        public string etat_prise_en_charge { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
    }
}
