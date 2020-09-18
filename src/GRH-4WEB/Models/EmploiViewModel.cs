using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GRH_4WEB.Models
{
    public class EmploiViewModel : SitualtionViewModel
    {
        
        [Required(ErrorMessage = "Le poste est requis")]
        public long poste_id { get; set; }
    }
}