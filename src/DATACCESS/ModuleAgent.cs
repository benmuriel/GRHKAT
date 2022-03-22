using DATACCESS.Models;
using DATACCESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;

namespace DATACCESS
{
    public static class ModuleAgent
    {
        private static GrhkatModel DB = new GrhkatModel();


        public static v_position_temporaire PositionTemporaireGet(long id)
        {
            DB = new GrhkatModel();
            return DB.v_position_temporaire.Find(id);
        }

        public static List<v_planning_project> PositionTemporairePlanningProjectLoad()
        {
            DB = new GrhkatModel();
            return DB.v_planning_project.OrderByDescending(e => e.created_at).ThenBy(e => e.locked_at).ToList();
        }
        public static agent_synthese ModuleAgentAgentSave(agent_synthese agent_synthese)
        {
            try
            {
                var result = DB.Database.SqlQuery<agent_synthese>("exec sp_moduleagent_agent_save @id=@_id, @nom=@_nom ,@post_nom =@_post_nom " +
                    ",@prenom =@_prenom ,@genre=@_genre ,@etat_civil= @_etat_civil ,@nationalite=@_nationalite ,@date_naissance=@_date_naissance ," +
                    "@lieu_naissance=@_lieu_naissance ,@telephone_personel_1=@_telephone_personel_1 ,@telephone_personel_2=@_telephone_personel_2 ," +
                    "@mail_personel_1=@_mail_personel_1 ,@mail_personel_2=@_mail_personel_2 ,@localisation_id=@_localisation_id ," +
                    "@id_pointeuse_user=@_id_pointeuse_user,@date_engagement = @_date_engagement "
                      ,
                  new SqlParameter("@_id", agent_synthese.agent_id),
                  new SqlParameter("@_nom", agent_synthese.nom),
                  new SqlParameter("@_post_nom", agent_synthese.post_nom),
              new SqlParameter { ParameterName = "@_prenom", Value = agent_synthese.prenom ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_genre", Value = agent_synthese.genre ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_etat_civil", Value = agent_synthese.etat_civil ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_nationalite", Value = agent_synthese.nationalite ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_date_naissance", Value = agent_synthese.date_naissance ?? SqlDateTime.Null },
              new SqlParameter { ParameterName = "@_lieu_naissance", Value = agent_synthese.lieu_naissance ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_telephone_personel_1", Value = agent_synthese.telepone_personel_1 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_telephone_personel_2", Value = agent_synthese.telephone_personel_2 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_mail_personel_1", Value = agent_synthese.mail_personel_1 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_mail_personel_2", Value = agent_synthese.mail_personel_2 ?? SqlString.Null },
              new SqlParameter { ParameterName = "@_localisation_id", Value = agent_synthese.localisation_id ?? SqlInt64.Null },
              new SqlParameter { ParameterName = "@_date_engagement", Value = agent_synthese.date_engagement ?? SqlDateTime.Null },
              new SqlParameter { ParameterName = "@_id_pointeuse_user", Value = agent_synthese.id_pointage ?? SqlInt64.Null });
                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);

            }
        }


        public static bool AgentDelete(long agent_id)
        {
            try
            {

                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_agent_delete @id", new SqlParameter("@id", agent_id));
                return true;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static List<planning_project> PositionTemporairePlanningProjectAvailableForAgentLoad(long type_position_id, long agent_id)
        {
            var result = DB.Database.SqlQuery<planning_project>("exec dbo.sp_moduleagent_situationagent_planning_project_available_for_agent_load @type_position_id=@_tp,@agent_id = @_ag",
             new SqlParameter { ParameterName = "@_tp", Value = type_position_id, DbType = System.Data.DbType.Int64 },
             new SqlParameter { ParameterName = "@_ag", Value = agent_id, DbType = System.Data.DbType.Int64 }
             );
            return result.ToList();
        }
        public static void PositionTemporairePlanningProjectSave(planning_project model)
        {
            planning_project entity = DB.planning_project.Find(model.id);


            if (entity == null)
            {
                entity = new planning_project();
                DB.planning_project.Add(entity);
            }
            entity.started_at = model.started_at;
            entity.ended_at = model.ended_at;
            entity.details = model.details;
            entity.type_position_id = model.type_position_id;
            entity.max_days = DB.type_position.Find(model.type_position_id).duree_max;
            entity.structures?.Clear();
            DB.SaveChanges();
            entity.structures = DB.structure.Where(e => model.selected_structures.Contains(e.id)).ToList();
            DB.SaveChanges();
        }

        public static void PositionTemporairePlanningProjectLock(long entity_id)
        {
            var result = DB.Database.ExecuteSqlCommand("exec dbo.sp_moduleagent_situationagent_planning_project_lock @id=@_id",
            new SqlParameter { ParameterName = "@_id", Value = entity_id, DbType = System.Data.DbType.Int64 });
        }

        public static List<v_situation_agent_required> SituationAgentRequiredLoad(long agent_id)
        {
            DB = new GrhkatModel();
            return DB.v_situation_agent_required.Where(e => e.agent_id == agent_id)
                .OrderByDescending(e => e.started_at).ThenBy(e => e.type_position_id).ToList();
        }
        public static List<v_position_temporaire> PositionTemporairePlanningProjectPositionsLoad(long projet_id)
        {
            DB = new GrhkatModel();
            return DB.v_position_temporaire.Where(e => e.planning_project_id == projet_id)
                .OrderBy(e => e.agent).ThenBy(e => e.started_at).ToList();
        }
        public static planning_project PositionTemporairePlanningProjectGet(long id)
        {
            DB = new GrhkatModel();
            return DB.planning_project.Include("structures").Include("type_position").Single(e => e.id == id);
        }

        public static DureePositionViewModel DureePosition(int id, int type_position_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<DureePositionViewModel>("exec dbo.sp_moduleagent_agent_duree_position @agent_id=@_id, @type_position_id = @_position_id",
              new SqlParameter { ParameterName = "@_id", Value = id, DbType = System.Data.DbType.Int64 },
              new SqlParameter { ParameterName = "@_position_id", Value = type_position_id, DbType = System.Data.DbType.Int32 }
              );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }
        public static int CountElement(long agent_id, long? type_id, string entity)
        {
            var result = DB.Database.SqlQuery<int>("exec dbo.sp_moduleagent_agent_count_element @agent_id=@_id, @type_id = @_type_id, @entity = @_entity",
           new SqlParameter { ParameterName = "@_id", Value = agent_id, DbType = System.Data.DbType.Int64 },
           new SqlParameter { ParameterName = "@_type_id", Value = type_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
           new SqlParameter { ParameterName = "@_entity", Value = entity ?? SqlString.Null, DbType = System.Data.DbType.String }
           );
            return result.SingleOrDefault();
        }

        public static agent_synthese AgentGet(long? id)
        {
            try
            {
                var result = DB.Database.SqlQuery<agent_synthese>("exec dbo.sp_moduleagent_agent_get @id",
                    new SqlParameter { ParameterName = "@id", Value = id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 }
                    );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static List<v_situation_agent_traitement> SituationAgentTraitementParSituation(long situation_id)
        {
            return DB.v_situation_agent_traitrement.Where(e => e.situation_agent_id == situation_id).ToList();
        } public static List<v_situation_agent_traitement> SituationAgentTraitementParAgent(long agent_id)
        {
            return DB.v_situation_agent_traitrement.Where(e => e.agent_id == agent_id).ToList();
        }

        public static List<agent_synthese> AgentLoad(string search = null, string order = "alpha", int limit = 100)
        {
            try
            {

                //  var result = DB.Database.SqlQuery<agent_synthese>("exec dbo.sp_moduleagent_agent_load @str_id= @_str_id, @tp_id= @_tp_id, @etat = @_etat,  @search = @_search,  @filter = @_filter",
                //new SqlParameter { ParameterName = "@_str_id", Value = str_id ?? SqlInt16.Null, DbType = System.Data.DbType.Int16 },
                //new SqlParameter { ParameterName = "@_tp_id", Value = tp_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
                //new SqlParameter { ParameterName = "@_search", Value = search ?? SqlString.Null, DbType = System.Data.DbType.String },
                //new SqlParameter { ParameterName = "@_etat", Value = etat ?? SqlString.Null, DbType = System.Data.DbType.String },
                //new SqlParameter { ParameterName = "@_filter", Value = profil ?? SqlString.Null, DbType = System.Data.DbType.String }
                //);
                using (GrhkatModel db = new GrhkatModel())
                {
                    var data = db.agent_synthese.Where(e => (search != null ? e.nom_complet.ToLower().Contains(search.ToLower()) : search == null));

                    switch (order)
                    {
                        case "saveasc":
                            data = data.OrderBy(e => e.created_at);
                            break;
                        case "savedesc":
                            data = data.OrderByDescending(e => e.created_at);
                            break;
                        default:
                            data = data.OrderBy(e => e.nom);
                            break;
                    }
                    if (limit > 0) data = data.Take(limit);
                    return data.ToList();
                }
            }

            catch (Exception e)
            {
                throw (e);

            };
        }

        public static List<v_position_temporaire> PositionTemporaireLoad(long? agent_id, string state, short? str_id, long? type_id)
        {
            DB = new GrhkatModel();
            try
            {
                if (str_id == 0) str_id = null;

                var result = DB.Database.SqlQuery<v_position_temporaire>("exec dbo.sp_moduleagent_position_temporaire_load @agent_id = @_agent_id, @str_id = @_str_id, @state = @_state, @tp_id = @_type_id",
              new SqlParameter { ParameterName = "@_agent_id", Value = agent_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
              new SqlParameter { ParameterName = "@_type_id", Value = type_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
              new SqlParameter { ParameterName = "@_str_id", Value = str_id ?? SqlInt16.Null, DbType = System.Data.DbType.Int16 },
              new SqlParameter { ParameterName = "@_state", Value = state ?? SqlString.Null, DbType = System.Data.DbType.String }
              );

                return result.OrderBy(e => e.isValidated).ThenByDescending(e => e.isRunning).ThenByDescending(e => e.is_required).ToList();
            }

            catch (Exception e)
            {
                throw (e);
            }
        }
        public static void SituationAgentPositionSave(v_position_temporaire entity)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_position_save " +
                    "@id=@_id, @agent_id= @_agent_id, @type_position_id = @_tp, @duree = @_duree, @reference= @_ref, " +
                    "@started_at = @_start_at, @value_data = @_value_data,  " +
                    "@lieu_realisation_position_id = @_lieu," +
                    "@details = @_details, @planning_project_id = @_plan",
                new SqlParameter("@_id", entity.id),
                new SqlParameter("@_agent_id", entity.agent_id),
                new SqlParameter("@_tp", entity.type_position_id),
                new SqlParameter("@_duree", entity.duree),
                new SqlParameter("@_start_at", entity.started_at),
                new SqlParameter("@_value_data", entity.value_data ?? SqlString.Null),
                new SqlParameter { ParameterName = "@_ref", Value = entity.start_reference, DbType = System.Data.DbType.String },
                 new SqlParameter { ParameterName = "@_lieu", Value = entity.lieu_realisation_position_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
                 new SqlParameter { ParameterName = "@_plan", Value = entity.planning_project_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
                new SqlParameter { ParameterName = "@_details", Value = entity.details ?? SqlString.Null, DbType = System.Data.DbType.String }
                  );

            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public static int SituationAgentValidate(long id, string request_for)
        {
            try
            {
                return DB.Database.ExecuteSqlCommand("exec [sp_moduleagent_situationagent_validate] @id = @_id, @request_for = @_rf",
                    new SqlParameter("@_id", id),
                    new SqlParameter("@_rf", request_for)
                    );
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static void SituationAgentDelete(long id)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_delete @id", new SqlParameter("@id", id));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static void SituationAgentUpdateTraitementSocial(long id)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_update_traitement_social @situation_id", new SqlParameter("@situation_id", id));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static void SituationAgentPositionTerminate(long id)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec [sp_moduleagent_situationagent_end_validation_request] @id",
                new SqlParameter("@id", id));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public static List<v_position_temporaire> WaitingValidationLoad(short tppos, string profil = null, long? agent_id = null)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_position_temporaire>("exec [sp_moduleagent_waiting_validations_load] @type_id= @str_id, @profil= @objet, @agent_id = @_ag",
                         new SqlParameter { ParameterName = "@str_id", Value = tppos },
                         new SqlParameter { ParameterName = "@_ag", Value = agent_id ?? SqlInt64.Null },
                         new SqlParameter { ParameterName = "@objet", Value = profil ?? SqlString.Null }

             );

                return result.ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }
        public static void ChargeSocialeSave(v_charge_sociale charge_Sociale)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_charge_sociale_save @id=@_id, @agent_id=@_agent_id, @nom =@_nom, " +
                    "@post_nom = @_poste_nom, @prenom =@_prenom, @occupation =@_occupation, @date_naissance = @_date_naiss," +
                    " @lieu_naissance =@_lieu_naiss ,@type_affinite_id = @_tpid ,@genre = @_genre"
                      ,
                  new SqlParameter("@_id", charge_Sociale.id),
                  new SqlParameter("@_agent_id", charge_Sociale.agent_id),
                  new SqlParameter("@_nom", charge_Sociale.nom),
                  new SqlParameter("@_poste_nom", charge_Sociale.post_nom ?? SqlString.Null),
                new SqlParameter { ParameterName = "@_prenom", Value = charge_Sociale.prenom ?? SqlString.Null },
                new SqlParameter { ParameterName = "@_occupation", Value = charge_Sociale.occupation ?? SqlString.Null },
                new SqlParameter { ParameterName = "@_date_naiss", Value = charge_Sociale.date_naissance },
                new SqlParameter { ParameterName = "@_lieu_naiss", Value = charge_Sociale.lieu_naissance ?? SqlString.Null },
                new SqlParameter { ParameterName = "@_tpid", Value = charge_Sociale.type_affinite_charge_sociale_id },
                new SqlParameter { ParameterName = "@_genre", Value = charge_Sociale.genre ?? SqlString.Null }
              );
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static void ChargeSocialeDelete(long id)
        {
            try
            {

                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_charge_sociale_delete @id", new SqlParameter("@id", id));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public static List<v_charge_sociale> ChargeSocialeLoad(long agent_id)
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
                throw (e);
            };
        }


    }
}
