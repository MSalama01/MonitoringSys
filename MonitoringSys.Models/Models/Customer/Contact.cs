using System.ComponentModel.DataAnnotations;

namespace MonitoringSys.Models
{
    public class Contact : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 255, MinimumLength = 3)]
        public string Name { get; set; }

        public string Mobile { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }
}
