using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GRH_4WEB.Models
{
    public class ChargeSocialViewModel
    {
        public long charge_id { get; set; }
        public long agent_id { get; set; }

        [Required]
        [Display(Name = "Nom (*)")]
        public string nom { get; set; }

        [Required]
        [Display(Name = "Postnom (*)")]
        public string post_nom { get; set; }

        [Display(Name = "Prénom (Optionel)")]
        public string prenom { get; set; }

        [Required]
        [Display(Name = "Genre (*)")]
        public string genre { get; set; }

        [Required]
        [Display(Name = "Lieu de naissance (*)")]
        public string lieu_naissance { get; set; }

        [Required]
        [Display(Name = "Date de naissance (*)")]

        public string date_naissance { get; set; }

        [Display(Name = "Etat de la prise (*)")]
        public bool etat { get; set; }


        [Display(Name = "Occupation actuelle (Optionnel)")]
        public string occupation { get; set; }

        [Required]
        [Display(Name = "Affinité (*)")]
        public int type_affinite_charge_sociale_id { get; set; }
        public string type_affinite_charge_sociale { get; set; }
    }
}