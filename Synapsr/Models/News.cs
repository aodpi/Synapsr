using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synapsr.Models
{
    [Table("news")]
    public class News
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string pic_url { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public DateTime date_published { get; set; }
    }
}