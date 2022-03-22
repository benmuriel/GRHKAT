using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    public class projet_engagement
    {
        public long id { get; set; }

        [Required (ErrorMessage ="Le nom du dossier est requis")]
        public string designation { get; set; }
        public bool inclu_la_retenu { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? closed_at { get; set; }


        public virtual ICollection<type_prise_en_charge> Type_Prise_En_Charges { get; set; }
        public virtual ICollection<departement> Departements { get; set; }
        public virtual ICollection<engagement> engagements { get; set; }

        public override string ToString()
        {
            return this.designation;
        }

        
    }
}
