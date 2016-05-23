using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Synapsr.Models
{
    [Table("users")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20),Required]
        public string UserName { get; set; }

        [StringLength(200), Required]
        public string Password { get; set; }

        [ForeignKey("Elevation"),DisplayName("Elevation"),Display(Name ="Elevation")]
        public int ElevationId { get; set; }
        [DisplayName("Elevation"),Display(Name ="Elevation")]
        public virtual Elevation Elevation { get; set; }

        public ICollection<NotificationChannel> NotificationChannels { get; set; }

        public string avatar_uri { get; set; }
    }
}