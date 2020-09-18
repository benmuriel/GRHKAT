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

        public agent_synthese()
        {
            type_carriere = "normal";
        }
        [Key]
        public long agent_id { get; set; }

        public string nom { get; set; }
        public string post_nom { get; set; }
        public string prenom { get; set; }
        public string nom_complet { get; set; }

        public string genre { get; set; }

        public string lieu_naissance { get; set; }

        public DateTime? date_naissance { get; set; }

        public string etat_civil { get; set; }

        public string nationalite { get; set; }

        public string numero_compte_salaire { get; set; }

        public string telepone_personel_1 { get; set; }

        public string telephone_personel_2 { get; set; }

        public string mail_personel_1 { get; set; }

        public string mail_personel_2 { get; set; }
        public long? localisation_id { get; set; }


        public long? id_pointage { get; set; }

        public long? carriere_id { get; set; }

        public string type_carriere { get; set; }

        public string matricule { get; set; }

        public DateTime? carriere_started_at { get; set; }

        public string carriere_reference { get; set; }

        public string grade_carriere { get; set; }
        public string grade_carriere_describe { get; set; }

        public string grade_carriere_reference { get; set; }

        public DateTime? grade_carriere_started_at { get; set; }

        public long? emploi_id { get; set; }

        public DateTime? emploi_started_at { get; set; }

        public string emploi_reference { get; set; }

        public long? poste_id { get; set; }
        public bool? is_politic { get; set; }

        public string fonction { get; set; }

        public string grade_fonction { get; set; }

        public short? service_id { get; set; }
        public string service { get; set; }



        //fin service

        public string etat_service { get; set; }
        public long? fin_carriere_id { get; set; }
        public string fin_carriere_motif { get; set; }
        public DateTime? fin_carriere_started_at { get; set; }
        public string fin_carriere_details { get; set; }
        public DateTime? fin_carriere_reference { get; set; }
        public DateTime? fin_carriere_validated_at { get; set; }

        public override string ToString()
        {
            return nom_complet;
        }
        public string fullEmploi()
        {
            return this.service_id == null ? " - " :
                this.fonction + " @ " + this.service;
        }
    }

    public enum Gender
    {
        Masculin, Feminin
    }
}
