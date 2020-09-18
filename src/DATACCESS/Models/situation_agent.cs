using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class situation_agent
    {
        [Key]
        public long id { get; set; }
        public long agent_id { get; set; }
        public DateTime started_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? ended_at { get; set; }
        public DateTime? start_validated_at { get; set; } 
        public string start_reference { get; set; }
        public string type_situation { get; set; }

        public bool isSaved
        {
            get { return id != 0; }
        }

        public string TypeSituation()
        {
            return this is v_emploi ? "Emploi" : this is carriere ? "Matricule" : this is grade_carriere ? "Grade de carrière"
                :"Position temporaire";
        }

        public string started_at_string
        {
            get
            {
                return this.started_at.Day + "/" + this.started_at.Month + "/" + this.started_at.Year;
            }
        } public string ended_at_string
        {
            get
            {
                 
                return this.ended_at == null?" durée indeterminée " : this.started_at.Day + "/" + this.started_at.Month + "/" + this.started_at.Year;
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
                return this.isValidated ? "Situation validée" : "En attente de validation";
            }
        }

        [NotMapped]
        public long save_id { 
            get {
                return this.id; 
            }
            set
            {
                this.id = value;
            }
        }
    }
}
