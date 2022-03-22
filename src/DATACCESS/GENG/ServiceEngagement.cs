using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG
{
    public static class ServiceEngagement
    {

        private static GengModel DB;

        public static IEnumerable<projet_engagement> projetEngagementLoad()
        {
            DB = new GengModel();
            return DB.projet_engagement.ToList();
        }

        public static IEnumerable<engagement> engagementLoad(long projet_id, int dept_id, int type_pes_id)
        {
            DB = new GengModel();
            return DB.engagement.Where(e => e.projet_id == projet_id
                     && e.beneficiaire.service_id == dept_id
                    && e.type_prise_en_charge_id == type_pes_id).ToList();
        }

        public static IEnumerable<beneficiaire> beneficiaireLoad(long projet_id, int dept_id, int type_pes_id)
        {
            throw new NotImplementedException();
        }

        public static projet_engagement projetEngagementGet(long? id)
        {
            DB = new GengModel();
            return DB.projet_engagement.Find(id);
        }

        public static void projetEngagementRemoveEngagement(long projet_id, long beneficiaire_id, int type_prise_en_charge_id)
        {
            DB = new GengModel();
            engagement item = DB.engagement.Find(projet_id, beneficiaire_id, type_prise_en_charge_id); 
            DB.engagement.Remove(item); 
            DB.SaveChanges();
        }

        public static void projetEngagementAddEngagement(long id, int tpid, string[] vs)
        {
            DB = new GengModel();
            type_prise_en_charge tp = DB.type_prise_en_charge.Find(tpid);
            for (int i = 0; i < vs.Length; i++)
            {
                long benid = Convert.ToInt64(vs[i]);
                eligibilite_prise_en_charge eli = DB.eligibilite_prise_en_charge.Single(e => e.type_prise_en_charge_id == tpid && e.beneficiaire_id == benid);
                engagement engagement = DB.engagement.SingleOrDefault(e => e.projet_id == id && e.type_prise_en_charge_id == tpid && e.beneficiaire_id == benid);

                if (engagement == null)
                {
                    engagement = new engagement
                    {
                        beneficiaire_id = benid,
                        projet_id = id,
                        type_prise_en_charge_id = tpid,
                        situation_agent_id = eli.situation_agent_id,
                        instance_id = tp.instance_execution_id,
                        created_at = DateTime.Now,
                    };
                    DB.engagement.Add(engagement);
                }
                // mettre a jours les infos
                //institution medical
                //bareme
                //montant + devise

                DB.SaveChanges();
            }

        }
        public static void projetEngagementAddTp(long id, int tpid)
        {
            DB = new GengModel();
            projet_engagement item = DB.projet_engagement.Find(id);
            type_prise_en_charge tp = DB.type_prise_en_charge.Find(tpid);
            item.Type_Prise_En_Charges.Add(tp);
            DB.SaveChanges();
        }

        public static void projetEngagementRemoveTp(long id, int tpid)
        {
            DB = new GengModel();
            projet_engagement item = DB.projet_engagement.Find(id);
            type_prise_en_charge tp = DB.type_prise_en_charge.Find(tpid);

            item.Type_Prise_En_Charges.Remove(tp);
            DB.engagement.Where(e => e.type_prise_en_charge_id == tp.id && e.projet_id == item.id).ToList().ForEach(ite =>
            DB.engagement.Remove(ite)
          );
            DB.SaveChanges();
        }

        public static void projetEngagementAddDep(long id, int depid)
        {
            DB = new GengModel();
            projet_engagement item = DB.projet_engagement.Find(id);
            departement tp = DB.departement.Find(depid);
            item.Departements.Add(tp);
            DB.SaveChanges();
        }

        public static void projetEngagementRemoveDep(long id, int tpid)
        {
            DB = new GengModel();
            projet_engagement item = DB.projet_engagement.Find(id);
            departement tp = DB.departement.Find(tpid);
            item.Departements.Remove(tp);
            DB.engagement.Where(e => e.beneficiaire.service_id == tp.id && e.projet_id == item.id).ToList().ForEach(ite =>
              DB.engagement.Remove(ite)
            );
         
            DB.SaveChanges();
        }

    }

}
