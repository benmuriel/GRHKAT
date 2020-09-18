using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class grade_carriere : situation_agent
    {
        public string grade_id { get; set; }
        public override string ToString()
        {
            return grade_id;
        }

    } 
}
