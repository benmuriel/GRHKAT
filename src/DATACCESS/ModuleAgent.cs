using DATACCESS.Models;
using DATACCESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;

namespace DATACCESS
{
    public static class ModuleAgent
    {
        private static GrhkatModel DB = new GrhkatModel();

        public static int SituationAgentValidate(long id)
        {
            try
            {
                return DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_validate @ids", new SqlParameter("@ids", id));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static void SituationAgentGadeCarriereSave(grade_carriere entity)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_gradecarriere_save @id, @agent_id,@grade_id, @reference , @date_signature",
                new SqlParameter("@id", entity.id),
                new SqlParameter("@agent_id", entity.agent_id),
                new SqlParameter("@grade_id", entity.grade_id),
                new SqlParameter("@reference", entity.start_reference),
                new SqlParameter("@date_signature", entity.started_at)
                 );
                DAO.SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);

            }
        }

        public static v_position_temporaire PositionTemporaireGet(long id)
        {
            return DB.v_position_temporaire.Find(id);
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



        public static int SituationAgentCarriereSave(carriere carriere)
        {
            try
            {
                return DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_carriere_save @id, @agent_id, @matricule , @reference , @date_signature, @type_carriere, @corps_metier_id",
                   new SqlParameter("@id", carriere.id),
                   new SqlParameter("@agent_id", carriere.agent_id),
                   new SqlParameter { ParameterName = "@matricule", Value = carriere.matricule ?? SqlString.Null },
                   new SqlParameter("@reference", carriere.start_reference),
                   new SqlParameter("@date_signature", carriere.started_at),
                   new SqlParameter { ParameterName = "@type_carriere", Value = carriere.type_carriere },
                   new SqlParameter { ParameterName = "@corps_metier_id", Value = carriere.coprs_metier_id ?? SqlInt64.Null }
                   );

            }
            catch (Exception e)
            {
                throw (e);
            }
        }


        public static List<agent_synthese> AgentLoad(short? str_id,string filter, string search)
        {
            try
            {
                var result = DB.Database.SqlQuery<agent_synthese>("exec dbo.sp_moduleagent_agent_load @str_id= @_str_id, @search = @_search,  @filter = @_filter",
              new SqlParameter { ParameterName = "@_str_id", Value = str_id ?? SqlInt16.Null, DbType = System.Data.DbType.Int16 },
              new SqlParameter { ParameterName = "@_search", Value = search ?? SqlString.Null, DbType = System.Data.DbType.String },
              new SqlParameter { ParameterName = "@_filter", Value = filter ?? SqlString.Null, DbType = System.Data.DbType.String }
              );

                return result.ToList();
            }

            catch (Exception e)
            {
                throw (e);

            };
        }

        public static List<v_position_temporaire> PositionTemporaireLoad(long? agent_id, string state, short? str_id, long? type_id)
        {
            try
            {//, 
                if (str_id == 0) str_id = null;

                var result = DB.Database.SqlQuery<v_position_temporaire>("exec dbo.sp_moduleagent_position_temporaire_load @agent_id = @_agent_id, @str_id = @_str_id, @state = @_state, @tp_id = @_type_id",
              new SqlParameter { ParameterName = "@_agent_id", Value = agent_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
              new SqlParameter { ParameterName = "@_type_id", Value = type_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
              new SqlParameter { ParameterName = "@_str_id", Value = str_id ?? SqlInt16.Null, DbType = System.Data.DbType.Int16 },
              new SqlParameter { ParameterName = "@_state", Value = state ?? SqlString.Null, DbType = System.Data.DbType.String }
              );

                return result.ToList();
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
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_position_save @id, @agent_id," +
             "@type_position_id, @duree, @reference , @date_signature, @ended_at, @adress_local_adresse,@poste_id, @inst_detach_id, @details",
                new SqlParameter("@id", entity.id),
                new SqlParameter("@agent_id", entity.agent_id),
                new SqlParameter("@type_position_id", entity.type_position_id),
                new SqlParameter("@duree", entity.duree),
                new SqlParameter { ParameterName = "@reference", Value = entity.start_reference, DbType = System.Data.DbType.String },
                new SqlParameter("@date_signature", entity.started_at),
                new SqlParameter { ParameterName = "@ended_at", Value = entity.ended_at ?? SqlDateTime.Null, DbType = System.Data.DbType.DateTime },
                new SqlParameter { ParameterName = "@adress_local_adresse", Value = entity.lieu_position_adresse ?? SqlString.Null, DbType = System.Data.DbType.String },
                new SqlParameter { ParameterName = "@poste_id", Value = entity.poste_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
                new SqlParameter { ParameterName = "@inst_detach_id", Value = entity.institution_detachement_id ?? SqlInt64.Null, DbType = System.Data.DbType.Int64 },
                new SqlParameter { ParameterName = "@details", Value = entity.details ?? SqlString.Null, DbType = System.Data.DbType.String }
                  );
                DAO.SuccessMessage();
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
                DB.Database.ExecuteSqlCommand("exec sp_moduleagent_situationagent_position_terminate @id",
                new SqlParameter("@id", id));
                DAO.SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public static List<v_all_validation> WaitingValidationLoad(short? currentStr, string objet)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_all_validation>("exec sp_moduleagent_validation_load @str_id, @objet",
                         new SqlParameter { ParameterName = "@str_id", Value = currentStr ?? SqlInt64.Null },
                         new SqlParameter { ParameterName = "@objet", Value = objet }

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
