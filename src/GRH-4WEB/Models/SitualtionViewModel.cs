using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GRH_4WEB.Models
{
    public class SitualtionViewModel
    {

        public long situation_id { get; set; }

        [Required(ErrorMessage = "La reference est requise")]
        public string start_reference { get; set; }

        [Required(ErrorMessage = "La date d'attribution est requise")]
        public string started_at { get; set; } 

        [Required(ErrorMessage = "L'agent est requis")]
        public long agent_id { get; set; }

        [Required(ErrorMessage = "Le type de position est requis")]
        public long type_position_id { get; set; }
         
        public long? planning_project_id { get; set; }
        [Required(ErrorMessage = "Une déscription est requis")]
        public string value_data { get; set; } 
      
        public int? motif_type_position_id { get; set; }
        public string motif_type_position { get; set; }
        // requirement 

        // require_lieu_realisation
        public int? lieu_realisation_position_id { get; set; }

        // require_end
        public string ended_at { get; set; }
          
        public string details { get; set; }
        public short duree_min { get; set; } 
        public short duree_max { get; set; } 
        public short duree { get; set; }
        public type_position type { get; set; }

       
    }
}