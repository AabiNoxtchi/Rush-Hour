using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Appointment:BaseEntity
    {
        public string Title { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        
        public int UserId { get; set; }

        public int ActivityId { get; set; }

        public virtual User user { get; set; }
        public virtual Activity activity { get; set; }
    }
}
