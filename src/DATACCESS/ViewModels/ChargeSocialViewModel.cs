using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.ViewModels
{
   public class ChargeSocialViewModel
    {
        public long id { get; set; }
        public long agent_id { get; set; }
      
        [Required]
        [Display(Name = "Nom")]
        public string nom { get; set; }
     
        [Required]
        [Display(Name = "Postnom")]
        public string post_nom { get; set; }

        [Display(Name = "Prénom")]
        public string prenom { get; set; } 

        [Display(Name = "Genre")]
        public string genre { get; set; }

        [Required]
        [Display(Name ="Lieu de naissance")]
        public string lieu_naissance { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]

        public DateTime date_naissance { get; set; }

        [Display(Name = "Etat de la prise")]
        public bool etat { get; set; }
        [Display(Name = "Occupation actuelle")]
        public string occupation { get; set; }

        [Display(Name = "Affinité avec l'agent")]
        public string affinite { get; set; }
    }
}
