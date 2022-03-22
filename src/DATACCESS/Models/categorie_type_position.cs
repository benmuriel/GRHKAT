using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class categorie_type_position
    {
        [Key]
        public string designation { get; set; }
        public string description { get; set; }
        public ICollection<type_position> type_positions { get; set; }
        
    }
}
