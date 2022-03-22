using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class type_position
    {
        public long id { get; set; }
        public string categorie { get; set; }
        public string designation { get; set; }
        public short duree_min { get; set; }                
        public short duree_max { get; set; }                
        public bool require_planning { get; set; }
        public bool require_end { get; set; }
        public bool require_report { get; set; }
        public bool require_realisation_lieu{ get; set; }
        public bool is_value_repeated { get; set; }
        public string value_origine_entity_type { get; set; }
        public string value_origine { get; set; }
        public bool is_required { get; set; }
        public string planning_validity_periode { get; set; }
        public ICollection<planning_project> planning_projects { get; set; } 
        public string duree_string
        {
            get
            {
                return this.duree_max == 0 ? "Durée indéterminée" : this.duree_max + " jour(s)";
            }
        }
        public categorie_type_position categorie_Type_Position { get; set; }

        public override string ToString()
        {
            return this.designation;
        }

        public string planning_validity_periode_string
        {
            get
            {
                switch(this.planning_validity_periode)
                {
                    case "custom":
                        return "Personnalisée";  
                    case "current_week":
                        return "La semaine en cours";  
                    case "current_month":
                        return "Le mois en cours";  
                    case "current_year":
                        return "l'année en cours";
                    default:
                        return "";
                }
            }
        }
        public bool is_custom_value
        {
            get
            {
                return this.value_origine == "custom";
            }
        }
        public bool is_entity_value
        {
            get
            {
                return this.value_origine == "entity";
            }
        }
        public bool is_computed_value
        {
            get
            {
                return this.value_origine == "computed";
            }
        }
        public bool is_motif_value
        {
            get
            {
                return this.value_origine == "motif";
            }
        }
    }
}
