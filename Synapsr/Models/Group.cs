using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Synapsr.Models
{
    [Table("grupe")]
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public virtual ICollection<RegCode> RegCodes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}