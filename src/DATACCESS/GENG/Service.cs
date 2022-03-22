using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG
{
   public static class Service
    {
        private static GengModel DB;

        public static int CountElement (string type,  long filter_id = 0)
        {
            DB = new GengModel();
            switch (type)
            {
                case "beneficiaire":
                    return DB.Beneficiaires.Count();

            }
            return 0;
        }

    }
}
