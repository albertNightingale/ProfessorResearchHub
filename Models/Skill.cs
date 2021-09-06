using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URC.Areas.Identity.Data;

namespace URC.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillText { get; set; }

        public virtual URCUser Student { get; set; }
    }
}
