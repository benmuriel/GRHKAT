using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GRH_4WEB.Models
{
    public class PositionTemporaireViewModel : SitualtionViewModel
    {
        
        [Required(ErrorMessage = "Le Type de position est requis")]
        public long type_position_id { get; set; }
        public int duree { get; set; }
        public string designation { get; set; } 
        public string ended_at { get; set; } 
        public string lieu_position_adresse { get; set; }
        public string details { get; set; } 
        public long? poste_id { get; set; }
        public long? institution_detachement_id { get; set; }

        public bool has_poste { get; set; }
        public bool has_institution { get; set; }

    }
}