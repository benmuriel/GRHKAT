using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    public class instance_traitement
    {
        public int id { get; set; }
        public string designation { get; set; }
        [JsonIgnore]
        public virtual ICollection<engagement> engagements { get; set; }
        [JsonIgnore]
        public virtual ICollection<type_prise_en_charge> TPCEngagement { get; set; }
        [JsonIgnore]
        public virtual ICollection<type_prise_en_charge> TPCExecution { get; set; }
        [JsonIgnore]
        public virtual ICollection<type_prise_en_charge> TPCLiquidation { get; set; }
        public override string ToString()
        {
            return this.designation;
        }
    }
}
