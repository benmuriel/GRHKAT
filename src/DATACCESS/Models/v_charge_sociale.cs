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
        public int age { get; set; }
        public string nom_complet { get; set; } 
        public string genre { get; set; } 
        public string occupation { get; set; } 
        public string lieu_naissance { get; set; } 
        public DateTime date_naissance { get; set; } 
       
        public int type_affinite_charge_sociale_id { get; set; }
        public string type_affinite_charge_sociale { get; set; }
         public bool etat_prise_en_charge_code { get; set; }
         public string etat_prise_en_charge { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? date_expiration_prise_charge { get; set; }
        public string state_color
        {
            get
            {
                return this.etat_prise_en_charge_code ? "green" 
                    : "red";
            }
        }
    }
}
