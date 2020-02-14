using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Activity:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Duration { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
