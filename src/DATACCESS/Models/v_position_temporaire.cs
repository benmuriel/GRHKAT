using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class v_position_temporaire : situation_agent
    {
        public long type_position_id { get; set; }
        public short duree { get; set; }
        public string agent { get; set; }
        public short? service_id { get; set; }
        public string service { get; set; }
        public string fonction { get; set; }
        public string matricule { get; set; }
        public string categorie { get; set; }
        public string type_position { get; set; }
        public string lieu_position_adresse { get; set; }
        public string details { get; set; }
        public long? institution_detachement_id { get; set; }
        public string institution_detachement { get; set; }
        public long? poste_id { get; set; }
        public string poste { get; set; }
        public string position_state { get; set; }
        public string Describe
        {
            get
            {
                return this.type_position + "\n"
                    + " " + (this.poste_id != null? " - "+this.poste :"")
                    + " " + (this.institution_detachement_id != null? " - "+this.institution_detachement :"")
                    + (String.IsNullOrWhiteSpace(this.details) ? "" : "(" + this.details + ")") + "\n"
                    + "Du  " + this.started_at.ToShortDateString() + "\n"
                    + (this.ended_at != null ? "Au  " + this.ended_at.Value.ToShortDateString()
                    + " - " + DAO.calculDuree(this.started_at, (DateTime)this.ended_at) : "à durée indéterminée");
            }
        }


        public string duree_string
        {
            get
            {
                return this.duree == 0 ? "Durée indéterminée" : this.duree + " jour(s)";
            }
        }
        public string state_string
        {
            get
            {
                return this.position_state == "tovalidate" ? "En attente de validation"
                    : this.position_state == "running" ? "En cours"
                    : this.position_state == "comming" ? "En attente de deroulement" 
                    : "Terminée";
            }
        }

        public string periode
        {
            get
            {
                return
                 "DU " + this.started_at.ToString("d/M/yyyy") + (this.ended_at == null ? " à ce jour" : " AU " + this.ended_at.Value.ToString("d/M/yyyy"));
            }
        }

        public override string ToString()
        {
            return this.type_position;
        }

       

    }
}