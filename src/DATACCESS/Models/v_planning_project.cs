using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.Models
{
    public class v_planning_project
    {

        public long id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? locked_at { get; set; }

        public DateTime started_at { get; set; } = DateTime.Now;
        public DateTime ended_at { get; set; } = DateTime.Now;

        public int max_days { get; set; }
        public string details { get; set; }
        public short structure_id { get; set; }
        public long type_position_id { get; set; }
        public string type_position { get; set; }
        public bool validity { get; set; }
        public bool current_str_agent_only { get; set; }
        public string structure { get; set; }
        public string planning_state { get; set; }
        public bool is_locked
        {
            get
            {
                return this.locked_at != null;
            }

        }
        public string validity_state
        {
            get
            {
                return this.validity ? "Valide" : "Expirée";
            }
        }

        public string created_at_string
        {
            get
            {
                return this.created_at.ToString("dd/MM/yyyy");
            }
        }
        public string locked_at_string
        {
            get
            {
                return this.locked_at?.ToString("dd/MM/yyyy");
            }
        }
        public string started_at_string
        {
            get
            {
                return this.started_at.ToString("dd/MM/yyyy");
            }
        }
        public string end_at_string
        {
            get
            {
                return this.ended_at.ToString("dd/MM/yyyy");
            }
        }
        public string designation
        {
            get
            {
                return "Plannification  " + this.started_at.ToString("dd/MM/yyyy") + " - " + this.ended_at.ToString("dd/MM/yyyy");
            }
        }

    }
}
