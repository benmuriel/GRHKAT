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
        public string reference { get; set; }

        [Required (ErrorMessage = "La date d'attribution est requise")]
        public string started_at { get; set; }

        [Required(ErrorMessage = "L'agent est requis")]
        public long agent_id { get; set; }
    }
}