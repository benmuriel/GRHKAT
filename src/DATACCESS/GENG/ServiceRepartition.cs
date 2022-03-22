using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG
{
    public static class ServiceRepartition
    {
        private static GengModel DB;

        public static List<beneficiaire> beneficiaireLoad( int page = 1)
        {
            DB = new GengModel();
            return DB.Beneficiaires.OrderBy(e => e.nom_complet).Skip((page - 1) * 50).Take(50).ToList();
        }

        public static IEnumerable<taux_change> tauxChangeLoad(string v)
        {
            DB = new GengModel();
            return DB.taux_change.Where(e => e.devise_id == v).OrderByDescending(e => e.created_at).ToList();
        }

       

        public static IEnumerable<eligibilite_prise_en_charge> eligibiliteValideLoad(int tp,int dept, string search)
        {
            DB = new GengModel();
            var data = DB.eligibilite_prise_en_charge.Where(e => e.type_prise_en_charge_id == tp && (e.occurence > 0 && e.occurence > e.done_occurence || e.occurence == 0 ) && ((dept == 0) || (dept != 0 && e.beneficiaire.service_id == dept)) && e.ended_at == null);
             
            return !String.IsNullOrWhiteSpace(search) ? data.Where(e=>e.beneficiaire.nom_complet.ToLower().Contains(search) || e.beneficiaire.matricule.ToLower().Contains(search) 
                || e.beneficiaire.fonction.ToLower().Contains(search)).ToList()
                : data.ToList();
        }

     
        public static IEnumerable<instance_traitement> instanceExcutionLoad()
        {
            DB = new GengModel();
            return DB.instance_execution.OrderBy(e => e.designation).ToList();
        }
        public static IEnumerable<departement> departementLoad()
        {
            DB = new GengModel();
            return DB.departement.OrderBy(e => e.designation).ToList();
        }

        public static taux_change tauxChangeGet(string v)
        {
            DB = new GengModel();
            return DB.taux_change.FirstOrDefault(d => d.devise_id == v && d.expired_at == null);
        }

    



        public static type_prise_en_charge typePriseEnChargeGet(int id)
        {
            DB = new GengModel();
            return DB.type_prise_en_charge.Find(id);
        }

        public static bareme baremeGet(string grade_id, int tpp_id)
        {
            DB = new GengModel();
            bareme bareme = DB.bareme.SingleOrDefault(e => e.grade_id == grade_id && e.type_prise_en_charge_id == tpp_id);
            if (bareme == null)
                bareme = new bareme
                {
                    grade_id = grade_id,
                    type_prise_en_charge_id = tpp_id,
                    devise_id = "USD",
                    type_prise_en_charge = DB.type_prise_en_charge.Find(tpp_id)
                };
            return bareme;
        }

        public static projet_engagement projetEngagementSave(projet_engagement projet_engagement)
        {
            DB = new GengModel();
            if (projet_engagement.id == 0)
            {
                projet_engagement.created_at = DateTime.Now;
                DB.projet_engagement.Add(projet_engagement);
            }
            else
            {
                projet_engagement p = DB.projet_engagement.Find(projet_engagement.id);
                p.designation = projet_engagement.designation;
                projet_engagement = p;
            }
            DB.SaveChanges();
            return projet_engagement;
        }

        public static void tauxChangeSave(taux_change modele)
        {
            taux_change current = tauxChangeGet(modele.devise_id);
            DB = new GengModel();
            current = DB.taux_change.Find(current.id);
            current.expired_at = DateTime.Today;
            modele.created_at = DateTime.Today;
            if (current.valeur != modele.valeur)
                DB.taux_change.Add(modele);
            DB.SaveChanges();
        }

      
        public static void baremeSave(bareme modele)
        {

            bareme current = baremeGet(modele.grade_id, modele.type_prise_en_charge_id);
            if (current.id != 0) //existe 
                current.expired_at = DateTime.Now;
            if (current.montant != modele.montant)//et montant different
                DB.bareme.Add(new bareme
                {
                    created_at = DateTime.Now,
                    devise_id = modele.devise_id,
                    type_prise_en_charge_id = modele.type_prise_en_charge_id,
                    grade_id = modele.grade_id,
                    montant = modele.montant
                });
            DB.SaveChanges();

        }
        public static void typePriseEnChargeSave(type_prise_en_charge modele)
        {
            var current = typePriseEnChargeGet(modele.id);
            current.instance_liquidation_id = modele.instance_liquidation_id;
            current.instance_engagement_id = modele.instance_engagement_id;
            current.instance_execution_id = modele.instance_execution_id;
            DB.SaveChanges();
        }

        public static beneficiaire beneficiaireGet(long id)
        {

            DB = new GengModel();
            return DB.Beneficiaires.Find(id);
        }

        public static List<grade> gradeLoad()
        {
            DB = new GengModel();
            return DB.grade.ToList();
        }
        public static List<type_prise_en_charge> typePriseEnChargeLoad()
        {
            DB = new GengModel();
            return DB.type_prise_en_charge.ToList();
        }
    }
}
