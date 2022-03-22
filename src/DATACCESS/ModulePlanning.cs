using DATACCESS.Models;
using DATACCESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS
{
    public static class ModulePlanning
    {
        private static GrhkatModel DB = new GrhkatModel();


        public static List<SituationAgentValueListItemViewModel> SituationAgentEntityValueListItemLoad(string value_origine_entity_type)
        {
            List<SituationAgentValueListItemViewModel> list = new List<SituationAgentValueListItemViewModel>();
            switch (value_origine_entity_type)
            {
                case "poste":
                    PosteVacantLoad(null).ForEach(item => list.Add(new SituationAgentValueListItemViewModel
                    {
                        description = item.ToString(),
                        id = item.id.ToString()
                    }));
                    break;
                case "grade":
                    GradeLoad().ForEach(item => list.Add(new SituationAgentValueListItemViewModel
                    {
                        description = item.ToString(),
                        id = item.id.ToString()
                    }));
                    break;
            }
            return list;
        }

        public static List<v_planning_project> PlanningPositionTemporaireLoad(int? type_position_id = null, short? str_id = 0, string state= "running")
        {          
            using (GrhkatModel db = new GrhkatModel())
                return db.v_planning_project.Where(e =>  
                 (((str_id != null && e.structure_id == str_id) || (str_id == null) )
                  && ((type_position_id != null && e.type_position_id == type_position_id) || (type_position_id == null)))
                  && e.planning_state == state
               ).OrderByDescending(e => e.validity).ToList();
            //using (GrhkatModel db = new GrhkatModel())
               // return db.v_planning_project.Where(e =>  
               //  (( str_id != null && e.structure_id == (short)str_id) || str_id == null)
               //   &&((type_position_id != null && e.type_position_id == (int)type_position_id) || type_position_id == null)
               //).OrderByDescending(e => e.validity).ToList();
        }

        public static List<v_structure> StructurePrincipaleLoad(string text)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_structure>("exec dbo.sp_moduleplanning_structure_load_principal @filter",
              new SqlParameter { ParameterName = "@filter", Value = text ?? SqlString.Null, DbType = System.Data.DbType.String }
              );
                return result.ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static type_affinite_charge_sociale TypeAffiniteChargeSocialGet(int id)
        {
            using (GrhkatModel DB = new GrhkatModel())
                return DB.type_affinite_charge_sociale.Find(id);
        }
        public static List<motif_type_position> TypesPositionTemporaireMotifLoad(long id)
        {
            using (GrhkatModel DB = new GrhkatModel())
                return DB.motif_type_position.Where(e => e.type_position_id == id).ToList();
        }

        public static List<lieu_realisation_type_position> StructureRealisation(long id)
        {
            using (GrhkatModel DB = new GrhkatModel())
                return DB.lieu_realisation_type_position.Where(e => e.type_position_id == id).ToList();
        }

        public static motif_type_position TypesPositionTemporaireMotifGet(int motifid)
        {
            using (GrhkatModel DB = new GrhkatModel())
                return DB.motif_type_position.Find(motifid);
        }

        public static type_position TypesPositionTemporaireGet(long id)
        {
            try
            {
                var result = DB.Database.SqlQuery<type_position>(" select * from type_position where id = @id ",
                    new SqlParameter { ParameterName = "@id", Value = id });
                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static List<v_structure> StructurePrincipaleLoadChildren(long id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_structure>(" select * from v_structure where parent_id = @id order by lvl, structure ",
                    new SqlParameter { ParameterName = "@id", Value = id });
                return result.ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static v_structure StructureGet(short str_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_structure>("exec dbo.sp_moduleplanning_structure_get @id",
                new SqlParameter { ParameterName = "@id", Value = str_id, DbType = System.Data.DbType.Int16 }
                );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static void PosteDelete(v_poste entity)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleplanning_poste_delete @id",
                new SqlParameter("@id", entity.id)
                );
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static List<grade> GradeLoad()
        {
            var result = DB.Database.SqlQuery<grade>("exec dbo.sp_moduleplanning_grade_load");
            return result.ToList();
        }

        public static grade GradeGet(string id)
        {
            try
            {
                var result = DB.Database.SqlQuery<grade>("exec dbo.sp_moduleplanning_grade_get @id",
                 new SqlParameter { ParameterName = "@id", Value = id, DbType = System.Data.DbType.String });

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static List<type_position> TypesPositionTemporaire(Boolean? require_planning = null, string categorie = null)
        {
            try
            {
                var result = DB.Database.SqlQuery<type_position>(" select * from type_position  where " +
                    "((@cat is not null and categorie = @cat) or (@cat is null)) and" +
                    "((@reqp is not null and require_planning = @reqp) or (@reqp is null)) " +
                    "order by is_required desc, value_origine_entity_type desc,designation",
                          new SqlParameter { ParameterName = "@reqp", Value = require_planning ?? SqlBoolean.Null, DbType = System.Data.DbType.Boolean },
                          new SqlParameter { ParameterName = "@cat", Value = categorie ?? SqlString.Null, DbType = System.Data.DbType.String });
                return result.ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static List<v_poste_planning> PostePlanningLoad(short? str_id, string filter)
        {
            long? id = str_id;
            try
            {
                var result = DB.Database.SqlQuery<v_poste_planning>("exec sp_moduleplanning_poste_planning_load @id, @filter",
                         new SqlParameter { ParameterName = "@id", Value = id ?? SqlInt64.Null },
          new SqlParameter { ParameterName = "@filter", Value = filter ?? SqlString.Null }


             );

                return result.ToList();
            }

            catch (Exception e)
            {
                throw (e);
            };
        }


        public static List<categorie_type_position> CategorieTypePosition()
        {
            return DB.categorie_type_position.Include("type_positions").OrderBy(e => e.description).ToList();
        }

     
        public static List<v_poste> PosteVacantLoad(short? str_id)
        {
            long? id = str_id;
            try
            {
                using (GrhkatModel db = new GrhkatModel())
                    return db.v_poste.Where(e => e.etat_poste == "vacant").OrderBy(e => e.fonction).ToList();

            }

            catch (Exception e)
            {
                throw (e);
            };
        }
        public static v_poste_planning PostePlanningGet(long poste_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_poste_planning>("select * from v_poste_planning where id = @id",
                         new SqlParameter { ParameterName = "@id", Value = poste_id, DbType = System.Data.DbType.Int64 }

             );

                return result.SingleOrDefault();
            }

            catch (Exception e)
            {
                throw (e);
            };
        }
        public static List<v_poste> PosteLoadByStr(long id, string state, string filter)
        {
            try
            {
                using (GrhkatModel db = new GrhkatModel())
                    return db.v_poste.Where(e => e.structure_id == id && (state != null ? e.etat_poste == state : state == null))
                        .OrderByDescending(e => e.agent_id).ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static v_poste PosteGet(long id)
        {
            try
            {

                using (GrhkatModel db = new GrhkatModel())
                    return db.v_poste.Find(id);
            }
            catch (Exception e)
            {
                DAO.ErrorMessage(e);
                return null;
            };
        }
        public static void PosteSave(v_poste poste)
        {

            try
            {
                using (GrhkatModel db = new GrhkatModel())
                {
                    poste po = db.poste.FirstOrDefault(e => e.id == poste.id);
                    if (po == null)
                    {
                        po = new poste { structure_id = poste.structure_id, created_at = DateTime.Now };
                        db.poste.Add(po);
                    }
                    po.grade_id = poste.grade_fonction;
                    po.fonction = poste.fonction.ToUpper().Trim(); 
                    po.is_politic = poste.is_politic;
                    db.SaveChanges(); 
                } 
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static fonction FonctionGet(int fonction_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<fonction>("exec dbo.sp_moduleplanning_fonction_get @fonction_id",
                 new SqlParameter { ParameterName = "@fonction_id", Value = fonction_id, DbType = System.Data.DbType.Int64 }
                 );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }
        public static type_structure TypeStructuregGet(long type_structure_id)
        {
            return DB.type_structures.Find(type_structure_id);
        }
        public static List<v_structure> StructureRatByStr(short structure_id)
        {
            try
            {
                var result = DB.Database.SqlQuery<v_structure>("exec dbo.sp_moduleplanning_structure_rattachement_load_by_str @str_id",
                    new SqlParameter { ParameterName = "@str_id", Value = structure_id, DbType = System.Data.DbType.Int16 }
                    );

                return result.ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static List<type_affinite_charge_sociale> TypeAffiniteChargeSocialLoad()
        {
            return DB.type_affinite_charge_sociale.ToList();
        }

    }
}
