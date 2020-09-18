using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GRH_4WEB.Models
{
    public class PosteViewModel
    {
        public long id { get; set; }
        public int fonction_id { get; set; }
        public short structure_id { get; set; }
        public string designation { get; set; }
    }
}