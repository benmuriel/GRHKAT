using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG.Models
{
    public class departement
    {
        public int id { get; set; }
        public string designation { get; set; }

        [JsonIgnore]
        public ICollection<projet_engagement> projet_Engagements { get; set; }

    }
}
