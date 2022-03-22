using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    public class taux_change
    {
        public int id { get; set; }
        public string devise_id { get; set; }
        public decimal valeur { get; set; } = 1;

        public DateTime created_at { get; set; }
        public DateTime? expired_at { get; set; }
    }
}
