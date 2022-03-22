using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    [Table("bareme")]
    public class bareme
    {
        public int id { get; set; }
        public string grade_id { get; set; }
        public int type_prise_en_charge_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? expired_at { get; set; }
        public decimal montant { get; set; }
        public string devise_id { get; set; }
        public virtual grade grade { get; set; } 
        
        [JsonIgnore]
        public virtual type_prise_en_charge type_prise_en_charge { get; set; }

        [JsonIgnore]
        public ICollection<engagement> engagements { get; set; }
    }
}
