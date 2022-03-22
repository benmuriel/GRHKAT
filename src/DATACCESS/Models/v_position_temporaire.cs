using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class v_position_temporaire
    {
        [Key]
        public long id { get; set; }
        public long agent_id { get; set; }
        public DateTime started_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? ended_at { get; set; }
        public DateTime? start_validated_at { get; set; }
        public DateTime? end_requested_at { get; set; }
        public string start_reference { get; set; } = "";

        public string agent { get; set; }
        public string description { get; set; }
        public long type_position_id { get; set; }
        public short duree { get; set; }

        public string categorie { get; set; }
        public string type_position { get; set; } 
        public string details { get; set; }
         public bool require_planning { get; set; }
        public bool require_end { get; set; }
        public bool require_report { get; set; }

        public string value_data { get; set; }
        public int? lieu_realisation_position_id { get; set; }
        public string lieu_realisation_position { get; set; }
         public string position_state { get; set; }
        public long? planning_project_id { get; set; }
        public bool is_required { get; set; }
        public string ended_at_string { get; set; }
        public string created_at_string { get; set; }
        public string end_requested_at_string { get; set; }
        public string started_at_string { get; set; }
        public bool isSaved
        {
            get { return id != 0; }
        }

        public bool isRunning
        {
            get
            {
                return this.position_state == "running";
            }
        }


        public bool isValidated
        {
            get
            {
                return this.start_validated_at != null;
            }
        }

        public string validation_state_string
        {
            get
            {
                return this.periode+"\r "+(this.isValidated ? "" : "En attente de validation");
            }
        }

        [NotMapped]
        public long save_id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Describe
        {
            get
            {
                return
                     this.description + ". \n"
                     + (String.IsNullOrWhiteSpace(this.details) ? "" : "(" + this.details + ")") + "\n";
            }
        }

        public string state_string
        {
            get
            {
                return this.position_state == "tovalidate_start" ? "En attente de validation (Debut)"
                    : this.position_state == "tovalidate_end" ? "En attente de validation (Fin)"
                    : this.position_state == "running" ? "En cours"
                    : this.position_state == "comming" ? "En attente de deroulement"
                    : this.position_state == "passed" ? "Terminée" : "";
            }
        }
        public string state_color
        {
            get
            {
                return this.position_state.Contains("tovalidate") ? "orange"
                    : this.position_state == "running" ? "green"
                    : this.position_state == "comming" ? "yellow"
                    : "red";
            }
        }

        public string periode
        {
            get
            {
                return
                 "Du " + this.started_at_string +
                 (this.ended_at == null ? " à " : " Au " + this.ended_at_string)
                 + " - " + this.duree_string;
            }
        }
        public string duree_string
        {
            get
            {
                return DAO.calculDuree(this.started_at, this.ended_at);
                // return this.duree == 0 ? "Durée indéterminée" : this.duree + " jour(s)";

            }
        }         
        public override string ToString()
        {
            return this.type_position;
        }
    }
}