using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    [Table("grade")]
    public class grade
    {
        public string id { get; set; }
        public string designation { get; set; }

        [JsonIgnore]
        public virtual ICollection<bareme> baremes { get; set; }
    }
}
