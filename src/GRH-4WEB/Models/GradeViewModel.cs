using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GRH_4WEB.Models
{
    public class GradeViewModel : SitualtionViewModel
    {
        
        [Required(ErrorMessage = "Le grade est requis")]
        public string grade_id { get; set; }
    }
}