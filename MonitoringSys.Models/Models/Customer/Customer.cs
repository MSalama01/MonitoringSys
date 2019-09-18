﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringSys.Models
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 255, MinimumLength = 3)]
        public string Name { get; set; }

        public string Mobile { get; set; }

        public string City { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
