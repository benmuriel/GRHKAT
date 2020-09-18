using DATACCESS.Models;
using DATACCESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS
{
    public static class DAO
    {
        private static GrhkatModel DB = new GrhkatModel();

        public static bool Modified { get; set; }

        public static long AGENTID { get; set; }


        public static string ShortString(string text)
        {
            if (text.Length > 40)
                return text.Substring(0, 40) + "...";
            return text;

        }




        public static List<type_structure> ModulePlanningTypeStructureLoad()
        {
            return DB.type_structures.ToList();
        }

      
        public static v_charge_sociale ModuleAgentChargeSocialeGet(long id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_charge_sociale>(" select * from v_charge_sociale where id = @id",
                    new SqlParameter { Value = id, ParameterName = "@id" }
                    );

                return result.Single<v_charge_sociale>();
            }
            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

        public static string calculDuree(DateTime debut, DateTime fin)
        {
            string duree = "";

            if (fin.Subtract(debut).TotalDays < 30)
                duree = fin.Subtract(debut).TotalDays.ToString() + " Jour(s)";
            else
            {
                duree = Math.Floor(fin.Subtract(debut).TotalDays / 30).ToString() + " Mois";
                if (fin.Subtract(debut).TotalDays % 30 != 0)
                    duree += " et " + (fin.Subtract(debut).TotalDays % 30) + " jour(s)";

            }
            return duree;
        }

        public static int ModuleAgentNotificationValidationGet()
        {
            try
            {
                var result = DB.Database.SqlQuery<int>(" select count(*) from v_all_validation");

                return result.Single<int>();
            }
            catch (Exception e)
            {
                ErrorMessage(e);
                return 0;
            };
        }





        public static void ModuleAgentChargeSocialeSave(ChargeSocialViewModel charge_Sociale)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_charge_sociale_save @id, @agent_id, @nom, @post_nom, @prenom, @occupation, " +
  "@date_naissance ,@lieu_naissance ,@affinite ,@genre ,@etat "
                      ,
                  new SqlParameter("@id", charge_Sociale.id),
                  new SqlParameter("@agent_id", charge_Sociale.agent_id),
                  new SqlParameter("@nom", charge_Sociale.nom),
                  new SqlParameter("@post_nom", charge_Sociale.post_nom),
                new SqlParameter { ParameterName = "@prenom", Value = charge_Sociale.prenom ?? SqlString.Null },
                new SqlParameter { ParameterName = "@occupation", Value = charge_Sociale.occupation ?? SqlString.Null },
                new SqlParameter { ParameterName = "@date_naissance", Value = charge_Sociale.date_naissance },
                new SqlParameter { ParameterName = "@lieu_naissance", Value = charge_Sociale.lieu_naissance ?? SqlString.Null },
                new SqlParameter { ParameterName = "@affinite", Value = charge_Sociale.affinite ?? SqlString.Null },
                new SqlParameter { ParameterName = "@genre", Value = charge_Sociale.genre ?? SqlString.Null },
                new SqlParameter { ParameterName = "@etat", Value = charge_Sociale.etat }
              );
                SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static void ModuleAgentChargeSocialeDelete(long id)
        {
            try
            {

                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_charge_sociale_delete @id", new SqlParameter("@id", id));
                SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public static List<v_charge_sociale> ModuleAgentChargeSocialeLoad(long agent_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_charge_sociale>("exec sp_moduleagent_charge_sociale_load @agent_id",
                         new SqlParameter { ParameterName = "@agent_id", Value = agent_id }
             );
                return result.ToList();
            }
            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

        public static List<v_emploi> ModuleAgentSituationAgentEmploisLoad(long agent_id, string mode)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_emploi>("exec sp_moduleagent_situationagent_emploi_load @agent_id, @mode",
                         new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 },
                         new SqlParameter { ParameterName = "@mode", Value = mode ?? SqlString.Null, DbType = System.Data.DbType.String }
             );
                return result.ToList();
            }

            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

        public static List<v_position_temporaire> ModuleAgentSituationAgentHistoriquePosition(long agent_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_position_temporaire>("exec sp_moduleagent_situationagent_historique_position @agent_id",
                         new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
             );
                return result.ToList();
            }

            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

        public static type_position GetTypePosition(long id)
        {
            return DB.type_position.Find(id);
        }

        public static void ModuleAgentSituationAgentEmploiSave(v_emploi entity)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_emploi_save @id, @agent_id,@poste_id, @reference , @date_signature",
                new SqlParameter("@id", entity.id),
                new SqlParameter("@agent_id", entity.agent_id),
               new SqlParameter("@poste_id", entity.poste_id),
                 new SqlParameter("@reference", entity.start_reference),
                new SqlParameter("@date_signature", entity.started_at)
                 );
                SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);

            }
        }

        public static List<grade_carriere> ModuleAgentSituationAgentHistoriqueGradeCarriere(long agent_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<grade_carriere>("exec sp_moduleagent_situationagent_historique_grade_carriere @agent_id",
                         new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
             );
                return result.ToList();
            }

            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

        public static List<carriere> ModuleAgentSituationAgentHistoriqueMatricule(long agent_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<carriere>("exec sp_moduleagent_situationagent_historique_carriere @agent_id",
                         new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
             );
                return result.ToList();
            }
            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

  
        public static List<v_emploi> ModuleAgentSituationAgentHistoriqueEmploiSecondaire(long agent_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_emploi>("exec sp_moduleagent_situationagent_historique_emploi_secondaire @agent_id",
                         new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
             );
                return result.ToList();
            }
            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

        public static List<v_emploi> ModuleAgentSituationAgentHistoriqueEmploiPrincipale(long agent_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_emploi>("exec sp_moduleagent_situationagent_historique_emploi_principal @agent_id",
                         new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
             );
                return result.ToList();
            }
            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }




        public static List<v_localisation> ModuleAgentLocalisationLoadLastChildren()
        {
            try
            {
                var result = DB.Database.SqlQuery<v_localisation>("exec dbo.sp_moduleagent_localisation_load_last_children"
             );

                return result.ToList();
            }

            catch (Exception e)
            {
                ErrorMessage(e);
                return null;
            };
        }

        public static agent_synthese ModuleAgentAgentSave(agent_synthese agent_synthese)
        {
            try
            {
                var result = DB.Database.SqlQuery<agent_synthese>("exec sp_moduleagent_agent_save @id, @nom ,@post_nom ,@prenom ,@genre ,@etat_civil ,@nationalite ," +
  "@date_naissance ,@lieu_naissance ,@telephone_personel_1 ,@telephone_personel_2 ,@mail_personel_1 ,@mail_personel_2 ," +
  "@localisation_id ,@id_pointeuse_user "
                      ,
                  new SqlParameter("@id", agent_synthese.agent_id),
                  new SqlParameter("@nom", agent_synthese.nom),
                  new SqlParameter("@post_nom", agent_synthese.post_nom),
              new SqlParameter { ParameterName = "@prenom", Value = agent_synthese.prenom ?? SqlString.Null },
              new SqlParameter { ParameterName = "@genre", Value = agent_synthese.genre ?? SqlString.Null },
              new SqlParameter { ParameterName = "@etat_civil", Value = agent_synthese.etat_civil ?? SqlString.Null },
              new SqlParameter { ParameterName = "@nationalite", Value = agent_synthese.nationalite ?? SqlString.Null },
              new SqlParameter { ParameterName = "@date_naissance", Value = agent_synthese.date_naissance ?? SqlDateTime.Null },
              new SqlParameter { ParameterName = "@lieu_naissance", Value = agent_synthese.lieu_naissance ?? SqlString.Null },
              new SqlParameter { ParameterName = "@telephone_personel_1", Value = agent_synthese.telepone_personel_1 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@telephone_personel_2", Value = agent_synthese.telephone_personel_2 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@mail_personel_1", Value = agent_synthese.mail_personel_1 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@mail_personel_2", Value = agent_synthese.mail_personel_2 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@localisation_id", Value = agent_synthese.localisation_id ?? SqlInt64.Null },
              new SqlParameter { ParameterName = "@id_pointeuse_user", Value = agent_synthese.id_pointage ?? SqlInt64.Null }
                        );
                SuccessMessage();
                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);

            }
        }


        public static v_emploi ModuleAgentSituationAgentEmploiTovalidate(long agent_id)
        {

            try
            {
                var result = DB.Database.SqlQuery<v_emploi>("exec dbo.sp_moduleagent_situationagent_emploi_tovalidate @agent_id",
                    new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
                    );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static grade_carriere ModuleAgentSituationAgentGradeCarriereTovalidate(long agent_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<grade_carriere>("exec dbo.sp_moduleagent_situationagent_gradecarriere_tovalidate @agent_id",
                    new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
                    );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static void ModuleAgentSituationAgentDelete(long id)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_delete @id", new SqlParameter("@id", id));
                SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static carriere ModuleAgentSituationAgentCarriereTovalidate(long agent_id)
        {
            //
            try
            {
                var result = DB.Database.SqlQuery<carriere>("dbo.sp_moduleagent_situationagent_carriere_current_tovalidate @agent_id",
                    new SqlParameter { ParameterName = "@agent_id", Value = agent_id, DbType = System.Data.DbType.Int64 }
                    );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }


        public static string ShortString(string text, int length = 70)
        {
            return text.Length > length ? text.Substring(0, length) + " ..." : text;
        }


        public static void SuccessMessage()
        {
            // MessageBox.Show("Operation effectué avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DAO.Modified = true;
        }
        public static string ErrorMessage(Exception e)
        {
            Modified = false;
            string msg = "Une erreur est survenue: \n\n";
            if (e != null)
            {
                msg += e.Message + "\n\n";
                if (e.InnerException != null)
                    msg += e.InnerException.Message;
            }
            return msg;

        }
    }
}