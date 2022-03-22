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

        public static string RandomString(string type, int length)
        {
            Random ran = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string nums = "0123456789";
            switch (type)
            {
                case "char": 
                    return new string(Enumerable.Repeat(chars, length).Select(r => r[ran.Next(r.Length)]).ToArray());
                 case "number": 
                    return new string(Enumerable.Repeat(nums, length).Select(r => r[ran.Next(r.Length)]).ToArray());
            }
            return new string(Enumerable.Repeat(chars+ nums, length).Select(r => r[ran.Next(r.Length)]).ToArray());
        }

        public static string UniqueRamdonString(string source, string serie)
        {
            source = source.Trim().Replace(' ', 'W').ToUpper();
            return serie +                       //2
               RandomString("number", 3) +  //6
               source.Substring(0,1) +             //1
               RandomString("char", 2) +    //4
               source.Substring(source.Length -1,1) +   //1
               RandomString("number", 3) +  //6
               RandomString("char", 2);     //2
        }

        public static List<type_structure> ModulePlanningTypeStructureLoad()
        {
            return DB.type_structures.ToList();
        }
        public  static IEnumerable<type_position> TypeSituationLoad(string categorie)
        {
            return DB.type_position.Where(e => e.categorie == categorie).ToList();
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

        public static string calculDuree(DateTime debut, DateTime? datefin)
        {
            string duree = "";
            if (datefin == null) duree = "Durée indéterminée";
            else
            {
                DateTime fin = (DateTime)datefin;
                 if (fin.Subtract(debut).TotalDays < 30)
                    duree = fin.Subtract(debut).TotalDays.ToString() + " Jour(s)";
                else
                {
                    duree = Math.Floor(fin.Subtract(debut).TotalDays / 30).ToString() + " Mois";
                    if (fin.Subtract(debut).TotalDays % 30 != 0)
                        duree += " et " + (fin.Subtract(debut).TotalDays % 30) + " jour(s)";

                }
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

   
        public static string ShortString(string text, int length = 70)
        {
            return text.Length > length ? text.Substring(0, length) + " ..." : text;
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