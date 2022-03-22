using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class agent_synthese
    {
        public DateTime created_at { get; set; }
        [Key]
        public long agent_id { get; set; }

        public string nom { get; set; }
        public string post_nom { get; set; }
        public string prenom { get; set; }
        public string nom_complet { get; set; }

        public string genre { get; set; }

        public string lieu_naissance { get; set; }

        public DateTime? date_naissance { get; set; }
        public DateTime? date_engagement { get; set; }

        public string etat_civil { get; set; }

        public string nationalite { get; set; }

        public string numero_compte_salaire { get; set; }

        public string telepone_personel_1 { get; set; }

        public string telephone_personel_2 { get; set; }

        public string mail_personel_1 { get; set; }

        public string mail_personel_2 { get; set; }

        public long? id_pointage { get; set; }
        public long? localisation_id { get; set; }
        public int? age { get; set; }
        public int? anciennete { get; set; }


        //-------------------------------------------

        //matricule

        //public long? matricule_id { get; set; }
        //public string matricule { get; set; }
        //public DateTime? matricule_started_at { get; set; }
        //public DateTime? matricule_validated_at { get; set; }
        //public string matricule_reference { get; set; }



        //grade

        //public long? grade_carriere_id { get; set; }
        //public string grade_carriere { get; set; }
        //public string grade_carriere_describe { get; set; }
        //public string grade_carriere_reference { get; set; }
        //public DateTime? grade_carriere_started_at { get; set; }
        //public DateTime? grade_carriere_validated_at { get; set; }


        //emploi

        //public long? emploi_id { get; set; }
        //public DateTime? emploi_started_at { get; set; }
        //public DateTime? emploi_validated_at { get; set; }
        //public string emploi_reference { get; set; }
        //public long? poste_id { get; set; }
        //public bool? is_politic { get; set; }
        //public string fonction { get; set; }
        //public string grade_fonction { get; set; }
        //public short? service_id { get; set; }
        //public string service { get; set; }

        public int total_situation_requis { get; set; }

        public int total_agent_situation_requis { get; set; }
        public int total_agent_situation { get; set; }
        public int total_charge_social { get; set; }

        public override string ToString()
        {
            return nom_complet;
        }
        //public string fullEmploi()
        //{
        //    return this.service_id == null ? " - " :
        //        this.fonction + " @ " + this.service;
        //}

        public string created_at_string
        {
            get
            {
               
                return this.created_at.ToString("dd/MM/yyyy hh:mm:ss");
            }
        }  public string date_naissance_string
        {
            get
            {
                if (this.date_naissance == null) return "-";
                return this.date_naissance?.Day + "/" + this.date_naissance?.Month + "/" + this.date_naissance?.Year;
            }
        }
        public string date_engagement_string
        {
            get
            {
                if (this.date_engagement == null) return "-";
                return this.date_engagement?.Day + "/" + this.date_engagement?.Month + "/" + this.date_engagement?.Year;
            }
        }
    }

    public enum Gender
    {
        Masculin, Feminin
    }
}
