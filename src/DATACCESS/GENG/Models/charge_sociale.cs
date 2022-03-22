using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    public class charge_sociale
    {
        public long id { get; set; }
        public string nom_complet { get; set; }
        public DateTime? date_naissance { get; set; }
        public string affinite { get; set; }
        public string occupation { get; set; }
        public long agent_id { get; set; }

        public DateTime created_at { get; set; }
        public DateTime? expired_at { get; set; }

        [JsonIgnore]
        public beneficiaire beneficiaire { get; set; }

        [JsonIgnore]
        public virtual ICollection<engagement> engagements { get; set; }

    }
}
