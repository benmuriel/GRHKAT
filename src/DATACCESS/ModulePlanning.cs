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

        public static List<ServicePosteVacantViewModel> GetPosteVacantServices()
        {
            try
            {
                var result = DB.Database.SqlQuery<ServicePosteVacantViewModel>(" select distinct structure_id , structure  from v_poste_vacant order by structure ");
                return result.ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }

        public static type_position TypesPositionTemporaireGet(int id)
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
        public static List<institution_detachement> InstitutionDetachementLoad()
        {
            try
            {
                return DB.institution_detachement.OrderBy(e => e.designation).ToList();
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
                DAO.SuccessMessage();
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

        public static List<type_position> TypesPositionTemporaire(string categorie = null)
        {
            try
            {
                var result = DB.Database.SqlQuery<type_position>(" select * from type_position  where (@cat is not null and categorie = @cat) or (@cat is null) order by designation",
                          new SqlParameter { ParameterName = "@cat", Value = categorie, DbType = System.Data.DbType.String });
                return result.ToList();
            }
            catch (Exception e)
            {
                throw (e);
            };
        }
        public static void StructureSave(v_structure entity)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleplanning_structure_save @id, @parent_id, @type_structure_id, " +
                    "@poste_id ,@designation ,@is_affectation ,@lvl ",
                  new SqlParameter("@id", entity.id),
                  new SqlParameter("@parent_id", entity.parent_id ?? SqlInt64.Null),
                  new SqlParameter("@type_structure_id", entity.type_structure_id ?? SqlInt64.Null),
                  new SqlParameter("@poste_id", entity.poste_id ?? SqlInt64.Null),
                  new SqlParameter("@designation", entity.designation),
                  new SqlParameter("@is_affectation", entity.is_affectation),
                  new SqlParameter("@lvl", entity.lvl));
                DAO.SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);
            }
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


        public static void FonctionDelete(fonction entity)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleplanning_fonction_delete @id",
                new SqlParameter("@id", entity.id));
                DAO.SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public static List<categorie_type_position> CategorieTypePosition()
        {
            return DB.categorie_type_position.Include("type_positions").OrderBy(e => e.description).ToList();
        }

        public static void FonctionSave(fonction entity)
        {
            try
            {
                DB.Database.ExecuteSqlCommand("exec sp_moduleplanning_fonction_save @id, @designation , @is_politic",
                new SqlParameter("@id", entity.id),
                new SqlParameter("@designation", entity.designation),
                new SqlParameter("@is_politic", entity.is_politic));
                DAO.SuccessMessage();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public static List<fonction> FonctionLoad(string text)
        {
            try
            {
                var result = DB.Database.SqlQuery<fonction>("exec dbo.sp_moduleplanning_fonction_load @filter",
                   new SqlParameter { ParameterName = "@filter", Value = text ?? SqlString.Null, DbType = System.Data.DbType.String }
                   );

                return result.ToList();
            }
            catch (Exception e)
            {
                DAO.ErrorMessage(e);
                return null;
            };

        }

        public static List<v_poste_vacant> PosteVacantLoad(short? str_id, string filter)
        {
            long? id = str_id;
            try
            {
                var result = DB.Database.SqlQuery<v_poste_vacant>("exec sp_moduleplanning_poste_load_vacant @str_id, @filter",
                         new SqlParameter { ParameterName = "@str_id", Value = id ?? SqlInt64.Null },
          new SqlParameter { ParameterName = "@filter", Value = filter ?? SqlString.Null }


             );

                return result.ToList();
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
                var result = DB.Database.SqlQuery<v_poste>("exec dbo.sp_moduleplanning_poste_load_by_str  @str_id= @_str_id, @filter = @_filter, @state = @_state",
                 new SqlParameter { ParameterName = "@_str_id", Value = id, DbType = System.Data.DbType.Int64 },
                 new SqlParameter { ParameterName = "@_filter", Value = filter ?? SqlString.Null, DbType = System.Data.DbType.String },
                 new SqlParameter { ParameterName = "@_state", Value = state ?? SqlString.Null, DbType = System.Data.DbType.String }
                );
                return result.ToList();
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
                var result = DB.Database.SqlQuery<v_poste>("exec dbo.sp_moduleplanning_poste_get @id",
                 new SqlParameter { ParameterName = "@id", Value = id, DbType = System.Data.DbType.Int64 }
                 );

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                DAO.ErrorMessage(e);
                return null;
            };
        }
        public static v_poste PosteSave(v_poste poste)
        {

            try
            {
                var result = DB.Database.SqlQuery<v_poste>("exec sp_moduleplanning_poste_save @id, @fonction_id, @designation , @str_id",
                new SqlParameter("@id", poste.id),
                new SqlParameter("@fonction_id", poste.fonction_id),
                new SqlParameter("@designation", poste.designation ?? SqlString.Null),
                new SqlParameter("@str_id", poste.structure_id)
                );              
                return result.SingleOrDefault();
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



    }
}
