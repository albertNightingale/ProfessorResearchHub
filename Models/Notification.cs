using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using URC.Areas.Identity.Data;

namespace URC.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsRead { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedTime { get; set; }
        
        public virtual URCUser User { get; set; }
    }
}
