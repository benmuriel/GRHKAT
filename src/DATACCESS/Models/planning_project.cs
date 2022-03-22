using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class planning_project
    {
        public planning_project()
        {
            this.selected_structures = this.structures?.Select(e => e.id).ToArray();
        }
        public long id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? locked_at { get; set; } 

        [Required]
        [Display(Name = "Debut")]
        public DateTime started_at { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Fin")]
        public DateTime ended_at { get; set; } = DateTime.Now;

        [Display(Name = "Nombre maximum de jours")]
        public int max_days { get; set; }

        [Display(Name = "Details (Optionnel)")]
        public string details { get; set; }

        [Required]
        [Display(Name = "Type de position  (*)")]
        public long type_position_id { get; set; }
        public type_position type_position { get; set; }

        public virtual ICollection<structure> structures { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "Structures concernées  (*)")]
        public short[] selected_structures { get; set; }


        public bool validity
        {
            get
            {
                return DateTime.Today >= this.started_at && DateTime.Today <= this.ended_at;
            }
        }
        public string validity_state
        {
            get
            {
                return this.validity ? "Valide" : "Expirée";
            }
        }
        public string designation
        {
            get
            {
                return "Plannification  " + this.started_at.ToString("dd/MM/yyyy") + " - " + this.ended_at.ToString("dd/MM/yyyy");
            }
        }
    }
}
