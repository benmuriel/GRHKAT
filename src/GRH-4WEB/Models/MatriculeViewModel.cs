using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GRH_4WEB.Models
{
    public class MatriculeViewModel : SitualtionViewModel
    {
        
        [Required(ErrorMessage = "Le matricule est requis")]
        public string matricule { get; set; }
    }
}