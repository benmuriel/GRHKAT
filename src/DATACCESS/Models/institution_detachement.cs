using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
   public class institution_detachement
    {
        [Key]
        public long id { get; set; }
        public string designation { get; set; }
        public string categorie { get; set; }
        public categorie_type_position categorie_Type_Position { get; set; }

    }
}
