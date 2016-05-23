using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Synapsr.Models
{
    [Table("elevations")]
    public class Elevation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int Id { get; set; }
        [StringLength(25),Required]
        public string ElevationName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}